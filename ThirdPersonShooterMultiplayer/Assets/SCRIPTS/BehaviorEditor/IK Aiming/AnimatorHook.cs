using System;
using UnityEngine;

namespace StateAction
{
    public class AnimatorHook : MonoBehaviour
    {
        #region Editor Variables

        [Header("Aiming properties")]
        public Transform leftHandTarget;
        public Transform shoulder;
        public Transform aimPivot;

        #endregion

        #region Private Variables

        // References
        private Animator animator;
        private StateManager states;

        // Ik targets
        private Transform rightHandTarget;
        private Vector3 lookDirection;
        private float aimPositionDistance = 15f;
        private float shoulderRotationSpeed = 15f;
        private float aimPositionHeightOffset = 1.4f;

        // Weights
        private float rightHandWeight;
        private float leftHandWeight;
        private float baseWeight;
        private float bodyWeight;

        // Recoil animation
        private bool recoilIsInit;
        private float recoilTime;
        private Vector3 basePosition;
        private Vector3 baseRotation;
        private Vector3 offsetPosition;
        private Vector3 offsetRotation;

        #endregion

        public void Initialize(StateManager state)
        {
            states = state;
            animator = GetComponent<Animator>();
            
            if (shoulder == null)
            {
                shoulder = animator.GetBoneTransform (HumanBodyBones.RightShoulder);
            }

            aimPivot = new GameObject().transform;
            aimPivot.name = "AimPivot";
            aimPivot.parent = states.transform;

            rightHandTarget = new GameObject().transform;
            rightHandTarget.name = "RightHandTarget";
            rightHandTarget.parent = aimPivot;

            states.movementProperties.aimPosition = states.transformInstance.position + transform.forward * aimPositionDistance;
            states.movementProperties.aimPosition.y += aimPositionHeightOffset;
        }

        #region Unity Methods

        private void OnAnimatorMove()
        {
            lookDirection = states.movementProperties.aimPosition - aimPivot.position;
            HandleShoulder();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            HandleWeights();

            animator.SetLookAtWeight(baseWeight, bodyWeight, 1, 1, 1);
            animator.SetLookAtPosition(states.movementProperties.aimPosition);

            if (leftHandTarget != null)
            {
                UpdateIK(AvatarIKGoal.LeftHand, leftHandTarget, leftHandWeight);
                UpdateIK(AvatarIKGoal.RightHand, rightHandTarget, rightHandWeight);
            }
        }

        private void UpdateIK(AvatarIKGoal goal, Transform target, float weight)
        {
            animator.SetIKPositionWeight(goal, weight);
            animator.SetIKRotationWeight(goal, weight);
            animator.SetIKPosition(goal, target.position);
            animator.SetIKRotation(goal, target.rotation);
        }

        private void Tick()
        {
            RecoilAnimation ();
        }

        #endregion

        #region Recoil Animation

        public void RecoilAnimation()
        {
            if (!recoilIsInit)
            {
                recoilIsInit = true;
                recoilTime = 0f;
                offsetPosition = Vector3.zero;
            }
        }

        public void RecoilActual()
        {
            if (recoilIsInit)
            {
                recoilTime += states.deltaTime * 3;
                if (recoilTime > 1)
                {
                    recoilTime = 1;
                    recoilIsInit = false;
                }

                // TODO: add offset position to recoil animation.
                // offsetPosition = Vector3.forward * states;
                // offsetRotation = Vector3.forward * states;

                rightHandTarget.localPosition = basePosition + offsetPosition;
                rightHandTarget.localEulerAngles = baseRotation + offsetRotation;
            }
        }

        #endregion

        #region Handle Base Weights

        private void HandleWeights()
        {
            if (states.stateProperties.isInteracting)
            {
                rightHandWeight = 0;
                leftHandWeight = 0;
                baseWeight = 0;
                return;
            }

            float tl_weight = 0;
            float tm_weight = 0;

            if (states.stateProperties.isAiming)
            {
                tm_weight = 1;
                bodyWeight = 0.4f;
            }
            else
            {
                bodyWeight = 0.3f;
            }

            leftHandWeight = (leftHandTarget != null) ? 1 : 0;

            Vector3 ld = states.movementProperties.aimPosition - states.transformInstance.position;
            float angle = Vector3.Angle(states.transformInstance.forward, ld);

            tl_weight = (angle < 76) ? 1 : 0;
            if (angle > 60) tm_weight = 0;

            if (!states.stateProperties.isAiming)
            {
                // TODO: set weight when character is not aiming 
            }

            baseWeight = Mathf.Lerp(baseWeight, tl_weight, states.deltaTime * 1);
            rightHandWeight = Mathf.Lerp(rightHandWeight, tm_weight, states.deltaTime * 9);
        }

        #endregion

        #region Handle Shoulders

        private void HandleShoulder()
        {
            HandleShoulderPosition();
            HandleShoulderRotation();
        }

        private void HandleShoulderPosition()
        {
            aimPivot.position = shoulder.position;
        }

        private void HandleShoulderRotation()
        {
            Vector3 targetDirection = lookDirection;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = aimPivot.forward;
            }

            Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
            aimPivot.rotation = Quaternion.Slerp(aimPivot.rotation, lookRotation, states.deltaTime * shoulderRotationSpeed);
        }

        #endregion


    }
}