using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
	[CreateAssetMenu (menuName = "Actions/Condition Actions/On Aiming")]
	public class OnAiming : Condition
	{
		public bool reverse;

		public override bool CheckCondition (StateManager state)
		{
			if (!reverse)
			{
				return state.stateProperties.isAiming;
			}

			return !state.stateProperties.isAiming;
		}
	}
}