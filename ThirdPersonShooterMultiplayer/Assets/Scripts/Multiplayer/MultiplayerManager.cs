using System.Collections;
using UnityEngine;

namespace Multiplayer
{
    public class MultiplayerManager : Photon.MonoBehaviour
    {
        public StateAction.RayBallistics ballistics;
        public static MultiplayerManager Singleton;
        private MultiplayerReferences multiplayerReferences;

        public MultiplayerReferences MultiplayerReferences { get { return multiplayerReferences; } }

        private void OnPhotonInstantiate (PhotonMessageInfo info)
        {
            Singleton = this;
            DontDestroyOnLoad (this.gameObject);
            multiplayerReferences = new MultiplayerReferences ();
            DontDestroyOnLoad (multiplayerReferences.referencesTransform.gameObject);

            InstantiateNetworkPrint ();
        }

        #region Manager Methods

        private void InstantiateNetworkPrint ()
        {
            Managers.PlayerProfile playerProfile = Managers.GameManagers.GetPlayerProfile ();
            object[] data = new object[1];
            data[0] = playerProfile.itemIds[0];

            GameObject networkPrintInstance = PhotonNetwork.Instantiate ("NetworkPrint", Vector3.zero, Quaternion.identity, 0, data);
        }

        public void AddNewPlayer (NetworkPrint networkPrint)
        {
            networkPrint.transform.parent = multiplayerReferences.referencesTransform;
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

        public void BroadcastShootWeapon (StateAction.StateManager states, Vector3 direction, Vector3 origin)
        {
            int photonId = states.photonID;
            photonView.RPC ("RPCShootWeapon", PhotonTargets.All, photonId, direction, origin);
        }

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

        [PunRPC]
        public void RPCShootWeapon (int photonId, Vector3 direction, Vector3 origin)
        {
            if (photonId == multiplayerReferences.localPlayer.photonID) return;

            PlayerHolder shooter = multiplayerReferences.GetPlayer (photonId);
            if (shooter != null)
            {
                ballistics.ClientShoot (shooter.stateManager, direction, origin);
            }
        }

        #endregion
    }
}
