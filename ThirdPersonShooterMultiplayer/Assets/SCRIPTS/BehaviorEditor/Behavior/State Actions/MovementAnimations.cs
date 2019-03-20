using UnityEngine;
using System.Collections;

namespace StateAction
{
	[CreateAssetMenu (menuName = "Actions/State Actions/Movement Animations")]
	public class MovementAnimations : StateActions
	{
		public string floatName;

		public override void Execute (StateManager states)
		{
			states.animatorInstance.SetFloat (floatName, states.movementProperties.speed, 0.2f, states.deltaTime);
		}
	}
}