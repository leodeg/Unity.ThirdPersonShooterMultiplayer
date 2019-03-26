using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateObject
{
    [CreateAssetMenu(menuName = "Variables/Bool")]
    public class BoolVariable : ScriptableObject
    {
        public bool value;

        private void OnEnable ()
        {
            value = false;
        }

        public void Set(bool v)
        {
            value = v;
        }

        public void Set(BoolVariable v)
        {
            value = v.value;
        }

        public void Reverse()
        {
            value = !value;
        }

        public bool Compare(bool v)
        {
            return v == value;
        }

        public bool Compare(BoolVariable v)
        {
            return value == v.value;
        }

    }
}
