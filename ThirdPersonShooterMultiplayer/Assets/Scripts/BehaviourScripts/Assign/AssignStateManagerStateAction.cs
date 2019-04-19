using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Assign/AssignStateManager")]
    public class AssignStateManagerStateAction : StateActions
    {
        public StateObject.StateVariable stateVariable;

        public override void Execute (StateManager states)
        {
            stateVariable.value = states;
        }
    }
}