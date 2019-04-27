using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Animations/UpdateAiming")]
    public class UpdateAiming : StateActions
    {
        public override void Execute (StateManager states)
        {
            states.animatorInstance.SetBool (states.animationHashes.aiming, states.currentState.isAiming);
            states.animatorInstance.SetBool (states.animationHashes.crouching, states.currentState.isCrouching);

        }
    }
}