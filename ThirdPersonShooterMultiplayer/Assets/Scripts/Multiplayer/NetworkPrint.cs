using UnityEngine;
using System.Collections;

namespace Multiplayer
{
    public class NetworkPrint : Photon.MonoBehaviour
    {
        public int photonId;
        public bool isLocal;

        private void OnPhotonInstantiate (PhotonMessageInfo info)
        {
            MultiplayerManager multiplayerManager = MultiplayerManager.Singleton;
            photonId = photonView.ownerId;
            isLocal = photonView.isMine;
            multiplayerManager.AddNewPlayer (this);
        }

        public void InstantiateControllers (int spawnPositionIndex)
        {
            GameObject inputUpdater = Instantiate (Resources.Load ("InputUpdater")) as GameObject;

            object[] data = new object[2];
            data[0] = photonId;
            data[1] = photonView.instantiationData[0];

            PhotonNetwork.Instantiate ("MultiplayerController", Vector3.zero, Quaternion.identity, 0, data);
        }
    }
}