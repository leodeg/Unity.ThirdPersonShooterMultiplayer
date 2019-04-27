using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Animations/UpdateMovementAnimation")]
    public class UpdateMovementAnimation : StateActions
    {
        public override void Execute (StateManager states)
        {
            if (states.currentState.isAiming)
            {
                states.animatorInstance.SetFloat (states.animationHashes.vertical, states.movementProperties.vertical, 0.2f, states.deltaTime);
                states.animatorInstance.SetFloat (states.animationHashes.horizontal, states.movementProperties.horizontal, 0.2f, states.deltaTime);
            }
            else
            {
                states.animatorInstance.SetFloat (states.animationHashes.vertical, states.movementProperties.moveAmount, 0.2f, states.deltaTime);
            }
        }
    }
}