using System.Collections;
using UnityEngine;

namespace Multiplayer
{
    public class MultiplayerListener : Photon.MonoBehaviour
    {
        [Header ("States")]
        public StateAction.State localState;
        public StateAction.StateActions initLocalPlayer;
        public StateAction.State clientState;
        public StateAction.StateActions initClientPlayer;

        private StateAction.StateManager states;

        private void OnPhotonInstantiate (PhotonMessageInfo messageInfo)
        {
            states = GetComponent<StateAction.StateManager> ();

            if (photonView.isMine)
            {
                states.SetCurrentState (localState);
                initLocalPlayer.Execute (states);
            }
            else
            {
                states.SetCurrentState (clientState);
                initClientPlayer.Execute (states);
            }
        }

        public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo messageInfo)
        {

        }
    }
}