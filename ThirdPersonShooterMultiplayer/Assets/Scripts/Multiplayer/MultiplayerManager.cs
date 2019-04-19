using System.Collections;
using UnityEngine;

namespace Multiplayer
{
    public class MultiplayerManager : Photon.MonoBehaviour
    {
        public static MultiplayerManager Singleton;
        private MultiplayerReferences multiplayerReferences;

        private void OnPhotonInstantiate (PhotonMessageInfo info)
        {
            Singleton = this;
            DontDestroyOnLoad (this.gameObject);

            multiplayerReferences = new MultiplayerReferences ();

            if (PhotonNetwork.isMasterClient)
            {
                InstantiateNetworkPrint ();
            }
        }

        #region Manager Methods

        private void InstantiateNetworkPrint ()
        {
            GameObject networkPrintInstance = PhotonNetwork.Instantiate ("NetworkPrint", Vector3.zero, Quaternion.identity, 0);
        }

        public void AddNewPlayer (NetworkPrint networkPrint)
        {
            networkPrint.transform.parent = multiplayerReferences.referencesParent;
            PlayerHolder holder = multiplayerReferences.AddNewPlayer (networkPrint);

            if (networkPrint.isLocal)
            {
                multiplayerReferences.localPlayer = holder;
            }
        }

        public void CreateController ()
        {
            multiplayerReferences.localPlayer.networkPrint.InstantiateControllers (multiplayerReferences.localPlayer.spawnPosition);
        }

        #endregion

        #region Callbacks

        public void BroadcastSceneChange ()
        {
            if (PhotonNetwork.isMasterClient)
            {
                photonView.RPC ("RPCSceneChange", PhotonTargets.All);
            }
        }

        #endregion

        #region RPCs

        [PunRPC]
        public void RPCSceneChange ()
        {
            // TODO: set spawn position from master
            Managers.MultiplayerLauncher.Singleton.LoadCurrentSceneActual ();
        }

        [PunRPC]
        public void RPCSetSpawnPositionToPlayer (int photonID, int spawnPositionIndex)
        {
            if (photonID == multiplayerReferences.localPlayer.photonID)
            {
                multiplayerReferences.localPlayer.spawnPosition = spawnPositionIndex;
            }
        }

        #endregion
    }
}
