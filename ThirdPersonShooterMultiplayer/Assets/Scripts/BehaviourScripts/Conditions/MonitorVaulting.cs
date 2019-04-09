using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Conditions/Vaulting/Monitor Vaulting")]
    public class MonitorVaulting : Condition
    {
        public float originOffset = 1;
        public float secondOriginOffset = 0.2f;
        public float rayForwardDistance = 1;
        public float rayHigherForwardDistance = 1;
        public float rayDownDistance = 1.5f;
        public float valutOffsetPosition = 2;

        public AnimationClip vaultWalkClip;

        public override bool CheckCondition (StateManager state)
        {
            bool result = false;

            RaycastHit hit;
            Vector3 origin = state.transform.position;
            origin.y += originOffset;
            Vector3 direction = state.transform.forward;

            Debug.DrawRay (origin, direction * rayHigherForwardDistance, Color.blue);
            if (Physics.Raycast (origin, direction, out hit, rayForwardDistance))
            {
                Vector3 secondOrigin = origin;
                secondOrigin.y += secondOriginOffset;

                Vector3 firstHit = hit.point;
                firstHit.y -= originOffset;
                Vector3 normalDirection = -hit.normal;

                Debug.DrawRay (secondOrigin, direction * rayHigherForwardDistance, Color.blue);
                if (Physics.Raycast (secondOrigin, direction, out hit, rayHigherForwardDistance))
                {

                }
                else
                {
                    Vector3 thirdOrigin = secondOrigin + (direction * rayHigherForwardDistance);
                    Debug.DrawRay (thirdOrigin, Vector3.down * rayDownDistance, Color.green);
                    if (Physics.Raycast (thirdOrigin, Vector3.down, out hit, rayDownDistance))
                    {
                        result = true;
                        state.animatorInstance.SetBool (state.animatorParameters.isInteracting, true);
                        state.animatorInstance.CrossFade (state.animationHashes.vaultWalk, 0.2f);

                        state.vaultData.animationLength = vaultWalkClip.length;
                        state.vaultData.startPosition = state.transformInstance.position;

                        state.vaultData.isInitialized = false;
                        state.currentState.isVaulting = true;

                        Vector3 endPosition = firstHit;
                        endPosition += normalDirection * valutOffsetPosition;
                        state.vaultData.endingPosition = endPosition;
                    }
                }

            }

            return result;
        }
    }
}