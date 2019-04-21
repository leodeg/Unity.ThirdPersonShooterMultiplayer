using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Physics/IsGroundedSphereCast")]
    public class IsGroundedSphereCast : StateActions
    {
        public float footOffset = 0.05f;
        public float sphereCastRadius = 0.3f;
        public float distanceToGround = 1.7f;
        public float onAirdDistanceToGround = 1.75f;
        public float groundCheckerHeightOffset = 0.7f;

        public override void Execute (StateManager states)
        {
            Vector3 origin = states.transformInstance.position;
            origin.y += groundCheckerHeightOffset;
            float currentDistance = distanceToGround;

            if (!states.currentState.isGround)
                currentDistance = onAirdDistanceToGround;

            RaycastHit hit;
            Debug.DrawRay (origin, Vector3.down * currentDistance, Color.green);
            if (Physics.SphereCast (origin, sphereCastRadius, Vector3.down,
                out hit, currentDistance, states.groundLayers))
            {
                states.currentState.isGround = true;
            }
            else
            {
                states.currentState.isGround = false;
            }
        }
    }
}