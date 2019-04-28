using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Multiplayer
{
    public class MultiplayerManager : Photon.MonoBehaviour
    {
        public float spawnTimer = 5.0f;
        public StateAction.RayBallistics ballistics;
        public static MultiplayerManager Singleton;

        private List<PlayerHolder> playersToRespawn = new List<PlayerHolder> ();


        public MultiplayerReferences MultiplayerReferences { get; private set; }

        private void OnPhotonInstantiate (PhotonMessageInfo info)
        {
            Singleton = this;
            DontDestroyOnLoad (this.gameObject);
            MultiplayerReferences = new MultiplayerReferences ();
            DontDestroyOnLoad (MultiplayerReferences.referencesTransform.gameObject);

            InstantiateNetworkPrint ();
        }

        private void Update ()
        {
            float deltaTime = Time.deltaTime;

            if (playersToRespawn.Count > 0)
            {
                foreach (PlayerHolder player in playersToRespawn)
                {
                    player.spawnTimer += deltaTime;

                    if (player.spawnTimer > spawnTimer)
                    {
                        player.spawnTimer = 0;
                        photonView.RPC ("RPCSpawnPlayer", PhotonTargets.All, player.photonID);
                        playersToRespawn.Remove (player);
                    }
                }
            }
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
            networkPrint.transform.parent = MultiplayerReferences.referencesTransform;
            PlayerHolder holder = MultiplayerReferences.AddNewPlayer (networkPrint);

            if (networkPrint.isLocal)
            {
                MultiplayerReferences.localPlayer = holder;
            }
        }

        public void CreateController ()
        {
            MultiplayerReferences.localPlayer.networkPrint.InstantiateControllers (MultiplayerReferences.localPlayer.spawnPosition);
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

        public void BroadcastKillPlayer (int photonId, int killerId)
        {
            photonView.RPC ("RPCReceiveKillPlayer", PhotonTargets.All, photonId, killerId);
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
            if (photonID == MultiplayerReferences.localPlayer.photonID)
            {
                MultiplayerReferences.localPlayer.spawnPosition = spawnPositionIndex;
            }
        }

        [PunRPC]
        public void RPCShootWeapon (int photonId, Vector3 direction, Vector3 origin)
        {
            if (photonId == MultiplayerReferences.localPlayer.photonID) return;

            PlayerHolder shooter = MultiplayerReferences.GetPlayer (photonId);
            if (shooter != null)
            {
                ballistics.ClientShoot (shooter.states, null, direction, origin);
            }
        }

        [PunRPC]
        public void RPCSpawnPlayer (int photonId)
        {
            PlayerHolder playerHolder = MultiplayerReferences.GetPlayer (photonId);

            if (playerHolder.states != null)
            {
                playerHolder.states.SpawnPlayer ();
            }
        }

        [PunRPC]
        public void RPCReceiveKillPlayer (int photonId, int killerId)
        {
            // Master client
            photonView.RPC ("RPCKillPlayer", PhotonTargets.All, photonId, killerId);
            playersToRespawn.Add (MultiplayerReferences.GetPlayer (photonId));
        }

        [PunRPC]
        public void RPCKillPlayer (int photonId, int killerId)
        {
            PlayerHolder playerHolder = MultiplayerReferences.GetPlayer (photonId);

            if (playerHolder.states != null)
            {
                playerHolder.states.KillPlayer ();
            }
        }

        #endregion
    }
}
