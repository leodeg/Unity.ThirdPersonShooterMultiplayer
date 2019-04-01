using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
	[CreateAssetMenu (menuName = "Inputs/Input Manager")]
	public class InputManager : Action
    {
        public InputAxis horizontal;
        public InputAxis vertical;
		public InputButton aimingInput;

		public float moveAmount;
		public Vector3 moveDirection;

		public StateObject.TransformVariable holderCameraTransfrom;
		public StateObject.TransformVariable viewCameraTransfrom;
		public StateObject.StateVariable playerStates;

        public override void Execute()
        {
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal.value) + Math.Abs(vertical.value));

			if (playerStates.value != null)
			{
				playerStates.value.movementProperties.horizontal = horizontal.value;
				playerStates.value.movementProperties.vertical = vertical.value;
				playerStates.value.movementProperties.moveAmount = moveAmount;
				playerStates.value.movementProperties.moveDirection = moveDirection;
				playerStates.value.movementProperties.cameraForward = viewCameraTransfrom.transform.forward;

				Ray aimingRay = new Ray (viewCameraTransfrom.transform.position, viewCameraTransfrom.transform.forward);
				playerStates.value.movementProperties.aimPosition = aimingRay.GetPoint(100);

				playerStates.value.stateProperties.isAiming = aimingInput.isPressed;
			}

			if (viewCameraTransfrom.transform != null)
			{
				moveDirection = viewCameraTransfrom.transform.forward * vertical.value;
				moveDirection += viewCameraTransfrom.transform.right * horizontal.value;
			}
		}
    }
}
