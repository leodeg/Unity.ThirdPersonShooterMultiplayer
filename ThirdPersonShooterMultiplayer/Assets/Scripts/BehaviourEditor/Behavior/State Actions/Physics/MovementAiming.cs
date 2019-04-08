using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Physics/Movement Aiming")]
    public class MovementAiming : StateActions
    {
        public float movementSpeed = 2;
        public float crouchSpeed = 2;

        public override void Execute (StateManager states)
        {
            states.rigidbodyInstance.drag = (states.movementProperties.moveAmount > 0.1f) ? 0 : 4;
            float currentSpeed = (states.currentState.isCrouching) ? crouchSpeed : movementSpeed;

            states.rigidbodyInstance.velocity = states.movementProperties.moveDirection;
            states.rigidbodyInstance.velocity *= (states.movementProperties.moveAmount * currentSpeed);
        }
    }
}