using System.Collections;
using UnityEngine;

namespace StateObject
{
	[CreateAssetMenu(menuName = "Variables/State")]
	public class StateVariable : ScriptableObject
	{
		public StateAction.StateManager value;
	}
}