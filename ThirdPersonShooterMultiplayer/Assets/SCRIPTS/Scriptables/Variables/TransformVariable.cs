using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateObject
{
    [CreateAssetMenu(menuName = "Variables/Transform")]
    public class TransformVariable : ScriptableObject
    {
        public Transform transform;

        public void Set(Transform v)
        {
            transform = v;
        }

        public void Set(TransformVariable v)
        {
            transform = v.transform;
        }

        public void Clear()
        {
            transform = null;
        }
    }
}
