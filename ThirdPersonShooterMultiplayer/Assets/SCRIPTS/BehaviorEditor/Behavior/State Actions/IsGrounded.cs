using System.Collections;
using UnityEngine;

namespace StateAction
{
	[CreateAssetMenu (menuName = "Actions/State Actions/Is Grounded")]
	public class IsGrounded : StateActions
	{
		public float footOffset = 0.05f;
		public float groudCheckerOffset = 0.7f;
		public float distanceToGround = 1.7f;

		public override void Execute (StateManager states)
		{
			Vector3 origin = states.transform.position;
			origin.y += groudCheckerOffset;

			RaycastHit hit;
			if (Physics.Raycast(origin, Vector3.down, out hit, distanceToGround))
			{
				states.transform.position = hit.point + (Vector3.up * footOffset);
			}
		}
	}
}