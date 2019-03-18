using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Move Forward")]
	public class MoveForward : StateActions
	{
		public float movementSpeed = 2;

		public override void Execute (StateManager states)
		{
			if (states.movementProperties.speed > 0.1f)
			{
				states.rigidbodyInstance.drag = 0;
			}
			else
			{
				states.rigidbodyInstance.drag = 4;
			}

			states.rigidbodyInstance.velocity = states.transformInstance.forward * states.movementProperties.speed * movementSpeed;
		}
	}
}
