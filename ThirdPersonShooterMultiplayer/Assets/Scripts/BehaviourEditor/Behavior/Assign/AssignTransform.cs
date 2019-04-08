using StateObject;
using UnityEngine;

namespace StateAction
{
	public class AssignTransform : MonoBehaviour
	{
		public TransformVariable variable;

		private void OnEnable ()
		{
			variable.transform = this.transform;
			Destroy (this);
		}
	}
}
