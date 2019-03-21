using UnityEngine;
using System.Collections;

namespace StateAction
{
	[CreateAssetMenu (menuName = "Actions/State Animator/Set Bool Parameter")]
	public class SetBoolParameter : StateActions
	{
		public string boolParameterName;
		public bool status;

		public override void Execute (StateManager states)
		{
			states.animatorInstance.SetBool (boolParameterName, status);
		}
	}
}