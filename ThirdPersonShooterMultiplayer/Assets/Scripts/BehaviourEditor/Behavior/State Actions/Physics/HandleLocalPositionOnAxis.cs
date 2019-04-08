using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Physics/Handle Local Position On Axis")]
    public class HandleLocalPositionOnAxis : Action
    {
        public enum Axis { x, y, z }

        public StateObject.TransformVariable targetTransfrom;
        public StateObject.BoolVariable targetBool;
        public float defaultValue = 6f;
        public float affectedValue = 4f;
        public Axis targetAxis;

        public float speedSpeed = 9f;
        private float actualValue;

        public override void Execute ()
        {
            if (targetTransfrom.transform == null)
            {
                Debug.LogError ("HandleZooming:: camera transform is null.");
                return;
            }

            float targetValue = defaultValue;

            if (targetBool.value)
            {
                targetValue = affectedValue;
            }

            actualValue = Mathf.Lerp (actualValue, targetValue, speedSpeed * Time.deltaTime);

            Vector3 targetPosition = targetTransfrom.transform.localPosition;
            switch (targetAxis)
            {
                case Axis.x: targetPosition.x = actualValue; break;
                case Axis.y: targetPosition.y = actualValue; break;
                case Axis.z: targetPosition.z = actualValue; break;
            }

            targetTransfrom.transform.localPosition = targetPosition;
        }
    }
}
