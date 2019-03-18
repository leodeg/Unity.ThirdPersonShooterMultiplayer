using UnityEngine;
using System.Collections;

namespace StateAction
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Rotate Based On Camera")]
	public class RotateBasedOnCamera : StateActions
	{
		public StateObject.TransformVariable cameraTransform;
		public float cameraSpeed;

		public override void Execute (StateManager states)
		{
			if (cameraTransform.transform == null)
			{
				Debug.LogWarning ("RotateBasedOnCamera::Execute::CameraTransform is null.");
				return;
			}

			float horizotal = states.movementProperties.horizontal;
			float vertical = states.movementProperties.vertical;

			Vector3 targetDirection = cameraTransform.transform.forward * vertical;
			targetDirection += cameraTransform.transform.right * horizotal;
			targetDirection.Normalize ();
			targetDirection.y = 0;

			if (targetDirection == Vector3.zero)
			{
				targetDirection = states.transform.forward;
			}

			Quaternion targetRotation = Quaternion.LookRotation (targetDirection);
			states.transform.rotation = Quaternion.Slerp (states.transform.rotation, targetRotation, states.deltaTime * states.movementProperties.speed * cameraSpeed);
		}
	}
}