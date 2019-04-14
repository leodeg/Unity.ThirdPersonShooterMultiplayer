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

        #endregion

        #region Manager Methods

        public void LoadMainMenu ()
        {
            StartCoroutine (LoadScene(mainMenuName, OnMainMenu));
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

        private IEnumerator LoadScene (string targetLevel, OnSceneLoaded callback = null)
        {
            yield return SceneManager.LoadSceneAsync (targetLevel, LoadSceneMode.Single);
            isLoading = false;
            callback?.Invoke ();
        }

        #endregion
    }
}