using Multiplayer;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class MultiplayerLauncher : Photon.PunBehaviour
    {
        [Header ("Multiplayer")]
        public static MultiplayerLauncher Singleton;
        public uint gameVersion = 1;
        public PhotonLogLevel logLevel = PhotonLogLevel.ErrorsOnly;

        [Header ("Variables")]
        public string mainMenuName = "Main";
        public StateObject.GameEvent onConnectedToMaster;
        public StateObject.GameEvent onJoinedRoom;
        public StateObject.BoolVariable isConnected;
        public StateObject.BoolVariable isMultiplayer;

        public delegate void OnSceneLoaded ();
        public OnSceneLoaded onSceneLoadedCallback;

        private bool isLoading;
        private bool isInGame;

        #region Initialize

        private void Awake ()
        {
            if (Singleton == null)
            {
                Singleton = this;
                DontDestroyOnLoad (this.gameObject);
            }
            else
            {
                Destroy (this.gameObject);
            }
        }

        private void Start ()
        {
            isConnected.value = false;
            ConnectToServer ();
        }

        public void ConnectToServer ()
        {
            PhotonNetwork.logLevel = logLevel;
            PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.automaticallySyncScene = false;
            PhotonNetwork.ConnectUsingSettings (this.gameVersion.ToString ());
        }

        #endregion

        #region Setup Methods

        public void OnMainMenu ()
        {
            isConnected.value = PhotonNetwork.connected;
            if (isConnected.value)
            {
                OnConnectedToMaster ();
            }
            else
            {
                ConnectToServer ();
            }
        }

        #endregion

        #region Photon Callbacks

        private void OnConnectedToServer ()
        {
            isConnected.value = true;
            onConnectedToMaster.Raise ();
        }

        private void OnFailedToConnectToMasterServer (UnityEngine.Networking.NetworkIdentity error)
        {
            isConnected.value = false;
            onConnectedToMaster.Raise ();
            Debug.LogError (error.ToString ());
        }

        private void InstantiateMultiplayerManager ()
        {
            PhotonNetwork.Instantiate ("MultiplayerManager", Vector3.zero, Quaternion.identity, 0);
        }

        public override void OnCreatedRoom ()
        {
            Multiplayer.Room room = ScriptableObject.CreateInstance<Multiplayer.Room> ();

            object sceneName = null;
            PhotonNetwork.room.CustomProperties.TryGetValue ("scene", out sceneName);
            room.sceneName = (string)sceneName;
            room.roomName = PhotonNetwork.room.Name;

            GameManagers.GetResourcesManager ().currentRoom.value = room;
            Debug.Log ("MultiplayerLauncher::OnCreatedRoom::Complete");
        }

        public override void OnJoinedRoom ()
        {
            onJoinedRoom.Raise ();
            InstantiateMultiplayerManager ();
            Debug.Log ("MultiplayerLauncher::OnJoinedRoom::Complete");
        }

        public override void OnJoinedLobby ()
        {
            StartCoroutine (RoomCheck ());
        }

        private IEnumerator RoomCheck ()
        {
            yield return new WaitForSeconds (3);

            if (!isInGame)
            {
                MatchMakingManager manager = MatchMakingManager.Singleton;
                RoomInfo[] rooms = PhotonNetwork.GetRoomList ();
                manager.AddMatches (rooms);

                yield return new WaitForSeconds (5);
                StartCoroutine (RoomCheck ());
            }
        }

        #endregion

        #region Manager Methods

        public void JoinLobby ()
        {
            PhotonNetwork.JoinLobby ();
        }

        public void LoadMainMenu ()
        {
            StartCoroutine (LoadScene (mainMenuName, OnMainMenu));
        }

        public void JoinRoom (RoomInfo roomInfo)
        {
            PhotonNetwork.JoinRoom (roomInfo.Name);
            isInGame = true;
        }

        public void LoadCurrentRoom ()
        {
            if (isConnected)
            {
                MultiplayerManager.Singleton.BroadcastSceneChange ();
            }
            else
            {
                Multiplayer.Room room = GameManagers.GetResourcesManager ().currentRoom.value;

                if (!isLoading)
                {
                    isLoading = true;
                    StartCoroutine (LoadScene (room.sceneName));
                }
            }
        }

        public void LoadCurrentSceneActual (OnSceneLoaded callback = null)
        {
            Multiplayer.Room room = GameManagers.GetResourcesManager ().currentRoom.value;

            if (!isLoading)
            {
                isLoading = true;
                StartCoroutine (LoadScene (room.sceneName, onSceneLoadedCallback));
            }
        }

        private IEnumerator LoadScene (string targetLevel, OnSceneLoaded callback = null)
        {
            yield return SceneManager.LoadSceneAsync (targetLevel, LoadSceneMode.Single);
            isLoading = false;

            if (callback != null)
                callback.Invoke ();
        }

        public void CreateRoom (Multiplayer.RoomButton button)
        {
            if (isMultiplayer.value)
            {
                if (!isConnected.value)
                {
                    // TODO: add logic for when not connected
                }
                else
                {
                    RoomOptions roomOptions = new RoomOptions ()
                    {
                        MaxPlayers = 5,
                        CustomRoomProperties = new ExitGames.Client.Photon.Hashtable {
                            {"scene", button.sceneName }
                        }
                    };

                    PhotonNetwork.CreateRoom (null, roomOptions, TypedLobby.Default);
                }
            }
            else // single player
            {
                Multiplayer.Room room = ScriptableObject.CreateInstance<Multiplayer.Room> ();
                room.sceneName = button.sceneName;
                GameManagers.GetResourcesManager ().currentRoom.SetRoom (room);
            }

            isInGame = true;
        }

        #endregion
    }
}