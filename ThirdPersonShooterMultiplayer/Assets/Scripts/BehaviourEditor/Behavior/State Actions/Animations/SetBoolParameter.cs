using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Animations/Set Bool Parameter")]
    public class SetBoolParameter : StateActions
    {
        public string boolParameterName;
        public bool status;

        [Header("Use Scriptable Object")]
        public bool useStatusVariable = true;
        public StateObject.BoolVariable statusVariable;

        public override void Execute (StateManager states)
        {
            if (useStatusVariable && statusVariable != null)
            {
                states.animatorInstance.SetBool (boolParameterName, statusVariable.value);
            }
            else
            {
                states.animatorInstance.SetBool (boolParameterName, status);
            }
        }
    }
}