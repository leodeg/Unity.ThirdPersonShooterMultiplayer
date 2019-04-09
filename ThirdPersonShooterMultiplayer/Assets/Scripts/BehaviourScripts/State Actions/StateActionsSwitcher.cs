using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/State Action Switcher")]
    public class StateActionsSwitcher : StateActions
    {
        public StateObject.BoolVariable targetState;
        public StateActions onFalseAction;
        public StateActions onTrueAction;

        public override void Execute (StateManager states)
        {
            if (targetState.value)
            {
                if (onTrueAction != null)
                {
                    onTrueAction.Execute (states);
                }
            }
            else
            {
                if (onFalseAction != null)
                {
                    onFalseAction.Execute (states);
                }
            }
        }
    }
}