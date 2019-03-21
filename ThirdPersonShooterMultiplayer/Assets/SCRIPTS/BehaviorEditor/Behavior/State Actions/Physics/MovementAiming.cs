using System.Collections;
using UnityEngine;

namespace StateAction
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Movement Aiming")]
	public class MovementAiming : StateActions
	{
		public float movementSpeed = 2;

		public override void Execute (StateManager states)
		{
			if (states.movementProperties.moveAmount > 0.1f)
			{
				states.rigidbodyInstance.drag = 0;
			}
			else
			{
				states.rigidbodyInstance.drag = 4;
			}

			states.rigidbodyInstance.velocity = states.movementProperties.moveDirection
				* ( states.movementProperties.moveAmount
				* movementSpeed );
		}
	}
}