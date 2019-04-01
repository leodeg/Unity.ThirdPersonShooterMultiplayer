using UnityEngine;

namespace StateAction
{
	public class OnEnableAssignStateManager : MonoBehaviour
	{
		public StateObject.StateVariable targetVarible;

		private void OnEnable ()
		{
			targetVarible.value = GetComponent<StateManager> ();
			Destroy (this);
		}
	}
}
