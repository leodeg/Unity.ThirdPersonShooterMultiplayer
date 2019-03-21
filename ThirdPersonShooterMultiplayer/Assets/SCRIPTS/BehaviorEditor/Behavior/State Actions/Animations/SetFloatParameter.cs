using UnityEngine;
using System.Collections;

namespace StateAction
{
	[CreateAssetMenu (menuName = "Actions/State Animator/Set Float Parameter")]
	public class SetFloatParameter : StateActions
	{
		public string floatParameterName;

		public override void Execute (StateManager states)
		{
			states.animatorInstance.SetFloat (floatParameterName, states.movementProperties.moveAmount, 0.2f, states.deltaTime);
		}
	}
}