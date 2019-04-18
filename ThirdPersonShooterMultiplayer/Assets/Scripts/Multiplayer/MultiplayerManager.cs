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

        private void InstantiateNetworkPrint ()
        {
            GameObject networkPrintInstance = PhotonNetwork.Instantiate ("NetworkPrint", Vector3.zero, Quaternion.identity, 0);
        }

        public void AddNewPlayer (NetworkPrint networkPrint)
        {
            PlayerHolder holder = multiplayerReferences.AddNewPlayer (networkPrint);
            if (networkPrint.isLocal)
            {
                multiplayerReferences.localPlayer = holder;
            }
        }
    }
}
