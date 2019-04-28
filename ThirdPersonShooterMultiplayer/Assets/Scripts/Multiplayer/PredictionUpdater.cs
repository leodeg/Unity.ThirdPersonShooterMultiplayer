using UnityEngine;
using System.Collections;
using StateAction;

namespace Multiplayer
{
    [CreateAssetMenu (menuName = "Multiplayer/PredictionUpdater")]
    public class PredictionUpdater : StateAction.StateActions
    {
        public override void Execute (StateManager states)
        {
            states.multiplayerListener.Prediction ();
        }
    }
}