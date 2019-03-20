using UnityEngine;
using System.Collections;

namespace StateAction
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Rotate Based On Camera")]
	public class RotateBasedOnCamera : StateActions
	{
		public float cameraSpeed;

		public override void Execute (StateManager states)
		{
			Vector3 targetDirection = states.movementProperties.moveDirection;
			targetDirection.y = 0;

			if (targetDirection == Vector3.zero)
			{
				targetDirection = states.transformInstance.forward;
			}

			Quaternion targetRotation = Quaternion.LookRotation (targetDirection);
			states.transformInstance.rotation = Quaternion.Slerp (states.transformInstance.rotation, targetRotation, states.deltaTime * states.movementProperties.speed * cameraSpeed);
		}
	}
}