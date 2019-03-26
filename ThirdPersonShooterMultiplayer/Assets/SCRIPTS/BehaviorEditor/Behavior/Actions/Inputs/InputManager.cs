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

		public StateObject.TransformVariable cameraTransfrom;
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
				playerStates.value.movementProperties.cameraForward = cameraTransfrom.transform.forward;

				Ray ray = new Ray (viewCameraTransfrom.transform.position, viewCameraTransfrom.transform.forward);
				playerStates.value.movementProperties.aimPosition = ray.GetPoint(100);

				playerStates.value.stateProperties.isAiming = aimingInput.isPressed;
			}

			if (cameraTransfrom.transform != null)
			{
				moveDirection = cameraTransfrom.transform.forward * vertical.value;
				moveDirection += cameraTransfrom.transform.right * horizontal.value;
			}
		}
    }
}
