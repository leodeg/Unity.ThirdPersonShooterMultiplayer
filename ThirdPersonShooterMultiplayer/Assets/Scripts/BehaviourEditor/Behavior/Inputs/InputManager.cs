using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Inputs/Input Manager")]
    public class InputManager : Action
    {
        [Header ("Movement")]
        public InputAxis horizontal;
        public InputAxis vertical;
        public InputButton crouchInput;

        [Header ("Weapon")]
        public InputButton aimingInput;
        public InputButton shootInput;
        public InputButton reloadInput;

        [Header ("Variables")]
        public StateObject.TransformVariable holderCameraTransfrom;
        public StateObject.TransformVariable viewCameraTransfrom;
        public StateObject.StateVariable playerStates;

        [HideInInspector] public float moveAmount;
        [HideInInspector] public Vector3 moveDirection;

        public override void Execute ()
        {
            moveAmount = Mathf.Clamp01 (Mathf.Abs (horizontal.value) + Math.Abs (vertical.value));

            if (playerStates.value != null)
            {
                UpdateAxis ();
                UpdateButtons ();
                UpdateWeaponReloading ();
                UpdateAimingRaycasting ();
                UpdatePlayerMovementProperties ();
            }

            if (viewCameraTransfrom.transform != null)
            {
                UpdatePlayerMovementDirection ();
            }
        }

        private void UpdateAxis ()
        {
            playerStates.value.movementProperties.horizontal = horizontal.value;
            playerStates.value.movementProperties.vertical = vertical.value;
        }

        private void UpdateAimingRaycasting ()
        {
            Ray aimingRaycasting = new Ray (viewCameraTransfrom.transform.position, viewCameraTransfrom.transform.forward);
            playerStates.value.movementProperties.aimPosition = aimingRaycasting.GetPoint (100);
        }

        private void UpdatePlayerMovementProperties ()
        {
            playerStates.value.movementProperties.moveAmount = moveAmount;
            playerStates.value.movementProperties.moveDirection = moveDirection;
            playerStates.value.movementProperties.cameraForward = viewCameraTransfrom.transform.forward;
            playerStates.value.currentState.isCrouching = crouchInput.isPressed;
        }

        private void UpdateButtons ()
        {
            playerStates.value.currentState.isAiming = aimingInput.isPressed;
            playerStates.value.currentState.isShooting = shootInput.isPressed;
        }

        private void UpdateWeaponReloading ()
        {
            playerStates.value.currentState.isReloading = reloadInput.isPressed;
            reloadInput.boolVariable.value = playerStates.value.currentState.isReloading;

            if (reloadInput.isPressed)
            {
                playerStates.value.currentState.SetReloading ();
            }
        }

        private void UpdatePlayerMovementDirection ()
        {
            moveDirection = viewCameraTransfrom.transform.forward * vertical.value;
            moveDirection += viewCameraTransfrom.transform.right * horizontal.value;
        }
    }
}
