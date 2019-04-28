using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/AnimatorControllerUpdater")]
    public class AnimatorControllerUpdater : StateActions
    {
        public override void Execute (StateManager states)
        {
            states.animatorController.Tick ();
        }
    }
}