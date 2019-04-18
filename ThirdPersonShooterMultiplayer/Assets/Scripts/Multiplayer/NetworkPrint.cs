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
            MultiplayerManager manager = MultiplayerManager.Singleton;
            photonId = photonView.ownerId;
            isLocal = photonView.isMine;
            manager.AddNewPlayer (this);
        }
    }
}