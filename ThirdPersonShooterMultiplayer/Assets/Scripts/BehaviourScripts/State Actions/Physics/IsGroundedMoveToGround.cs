using System.Collections;
using UnityEngine;

namespace StateAction
{
	[CreateAssetMenu (menuName = "Actions/Physics/IsGroundedMoveToGround")]
	public class IsGroundedMoveToGround : StateActions
	{
		public float footOffset = 0.05f;
		public float groundCheckerHeightOffset = 0.7f;
		public float distanceToGround = 1.7f;

		public override void Execute (StateManager states)
		{
			Vector3 origin = states.transformInstance.position;
			origin.y += groundCheckerHeightOffset;

			RaycastHit hit;
			if (Physics.Raycast(origin, Vector3.down, out hit, distanceToGround, states.groundLayers))
			{
				Vector3 targetPosition = hit.point;
				targetPosition.x = states.transformInstance.position.x;
				targetPosition.z = states.transformInstance.position.z;
				targetPosition.y += footOffset;

				states.transform.position = targetPosition;
			}
		}
	}
}