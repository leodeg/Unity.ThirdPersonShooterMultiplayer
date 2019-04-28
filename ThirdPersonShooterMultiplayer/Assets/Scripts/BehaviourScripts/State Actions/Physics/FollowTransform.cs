using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateObject;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Physics/Follow Transform")]
    public class FollowTransform : Action
    {
        public FloatVariable deltaTime;
        public TransformVariable targetTransform;
        public TransformVariable currentTransform;
        public float speed = 9;


		public override void Execute ()
		{

			if (targetTransform.transform == null)
			{
				return;
			}

			if (currentTransform.transform == null)
			{
				return;
			}

			currentTransform.transform.position = Vector3.Lerp (currentTransform.transform.position, targetTransform.transform.position, deltaTime.value * speed);
		}
	}
}
