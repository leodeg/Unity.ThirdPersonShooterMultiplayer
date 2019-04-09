using UnityEngine;
using System.Collections;

namespace StateAction
{
	[CreateAssetMenu (menuName = "Actions/Animations/Set Movement Parameter")]
	public class SetMovementAnimatorParameter : StateActions
	{
		public string floatParameterName;

		public override void Execute (StateManager states)
		{
			states.animatorInstance.SetFloat (floatParameterName, states.movementProperties.moveAmount, 0.2f, states.deltaTime);
		}
	}
}