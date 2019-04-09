using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Update Animator Hook")]
    public class AnimatorControllerUpdater : StateActions
    {
        public override void Execute (StateManager states)
        {
            states.animatorController.Tick ();
        }
    }
}