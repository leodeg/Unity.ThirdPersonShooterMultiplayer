using System;
using System.Collections;
using System.Collections.Generic;
using StateObject;
using UnityEngine;

namespace StateAction
{
	public class AssignTransform : MonoBehaviour
	{
		public TransformVariable transformVariable;

		private void OnEnable ()
		{
			transformVariable.transform = this.transform;
			Destroy (this);
		}
	}
}
