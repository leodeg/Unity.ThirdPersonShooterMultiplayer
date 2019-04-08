using UnityEngine;
using System.Collections;

namespace StateAction
{
	[CreateAssetMenu(menuName = "Actions/Physics/Rotate Always At Camera Look")]
	public class RotateAlwaysAtCameraLook : StateActions
	{
		public float rotationSpeed = 5;

		public override void Execute (StateManager states)
		{
			Vector3 targetDirection = states.movementProperties.cameraForward;
			targetDirection.y = 0;

			if (targetDirection == Vector3.zero)
			{
				targetDirection = states.transformInstance.forward;
			}

			Quaternion targetRotation = Quaternion.LookRotation (targetDirection);
			states.transformInstance.rotation = Quaternion.Slerp (states.transformInstance.rotation, targetRotation, states.deltaTime * rotationSpeed);
		}
	}
}