using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
	[CreateAssetMenu (menuName = "Conditions/On Aiming")]
	public class OnAiming : Condition
	{
		public bool reverse;

		public override bool CheckCondition (StateManager state)
		{
			if (!reverse)
			{
				return state.currentState.isAiming;
			}

			return !state.currentState.isAiming;
		}
	}
}