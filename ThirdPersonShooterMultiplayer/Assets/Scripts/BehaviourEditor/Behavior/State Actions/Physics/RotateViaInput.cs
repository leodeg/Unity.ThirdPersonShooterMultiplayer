using System.Collections;
using UnityEngine;

namespace StateAction
{
	[CreateAssetMenu (menuName = "Actions/Physics/Rotate Via Input")]
	public class RotateViaInput : Action
	{
		public InputAxis targetInput;
		public StateObject.TransformVariable targetTransform;

		public float angle;
		public float sensitivity;
		public bool inverseInput;
		public bool clamp;
		public float minClamp = -35;
		public float maxClamp = 35;

		public enum RotateAxis { x, y, z }
		public RotateAxis rotateAboutAxis;


		public override void Execute ()
		{
			if (inverseInput)
			{
				angle -= targetInput.value * sensitivity;
			}
			else
			{
				angle += targetInput.value * sensitivity;
			}

			if (clamp)
			{
				angle = Mathf.Clamp (angle, minClamp, maxClamp);
			}

			Vector3 targetRotation = targetTransform.transform.localRotation.eulerAngles;

			switch (rotateAboutAxis)
			{
				case RotateAxis.x:
					targetRotation.x = angle;
					break;

				case RotateAxis.y:
					targetRotation.y = angle;
					break;

				case RotateAxis.z:
					targetRotation.z = angle;
					break;

				default: break;
			}

			targetTransform.transform.localRotation = Quaternion.Euler(targetRotation);
		}
	}
}