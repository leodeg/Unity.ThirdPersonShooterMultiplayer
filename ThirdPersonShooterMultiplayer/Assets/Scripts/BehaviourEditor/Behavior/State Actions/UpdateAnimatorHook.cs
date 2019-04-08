using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Update Animator Hook")]
    public class UpdateAnimatorHook : StateActions
    {
        public override void Execute (StateManager states)
        {
            states.animatorController.Tick ();
        }
    }
}