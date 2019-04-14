using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StateAction
{
    public class MultiplayerLauncher : Photon.PunBehaviour
    {
        [Header ("Multiplayer")]
        public static MultiplayerLauncher singleton;
        public uint gameVersion = 1;
        public PhotonLogLevel logLevel = PhotonLogLevel.ErrorsOnly;

        [Header ("Variables")]
        public string mainMenuName = "Main";
        public StateObject.GameEvent onConnectedToMaster;
        public StateObject.BoolVariable isConnected;
        public StateObject.BoolVariable isMultiplayer;

        private bool isLoading;
        private delegate void OnSceneLoaded ();

        #region Initialize

        private void Awake ()
        {
            if (singleton == null) singleton = this;
            else Destroy (this.gameObject);
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

        public override void OnCreatedRoom ()
        {
            Room room = ScriptableObject.CreateInstance<Room> ();

            object sceneName = null;
            PhotonNetwork.room.CustomProperties.TryGetValue ("scene", out sceneName);
            room.sceneName = (string)sceneName;
            room.roomName = PhotonNetwork.room.Name;

            GameManagers.GetResourcesManager ().currentRoom.value = room;
            Debug.Log ("OnCreatedRoom");
        }

        public override void OnJoinedRoom ()
        {

        }

        #endregion

        #region Manager Methods

        public void LoadMainMenu ()
        {
            StartCoroutine (LoadScene (mainMenuName, OnMainMenu));
        }

        public void LoadCurrentRoom ()
        {
            Room room = GameManagers.GetResourcesManager ().currentRoom.value;

            if (!isLoading)
            {
                isLoading = true;
                StartCoroutine (LoadScene (room.sceneName));
            }
        }

        public void CreateRoom (RoomButton button)
        {
            if (isMultiplayer.value)
            {
                if (!isConnected.value)
                {
                    // TODO: add logic for when not connected

                }
                else
                {
                    RoomOptions roomOptions = new RoomOptions ();
                    roomOptions.MaxPlayers = 5;
                    roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable
                    {
                        {"scene", button.sceneName }
                    };
                    PhotonNetwork.CreateRoom (null, roomOptions, TypedLobby.Default);
                }
            }
            else // single player
            {
                Room room = ScriptableObject.CreateInstance<Room> ();
                room.sceneName = button.sceneName;
                GameManagers.GetResourcesManager ().currentRoom.SetRoom (room);
            }
        }

        private IEnumerator LoadScene (string targetLevel, OnSceneLoaded callback = null)
        {
            yield return SceneManager.LoadSceneAsync (targetLevel, LoadSceneMode.Single);
            isLoading = false;
            callback?.Invoke ();
        }

        #endregion
    }
}