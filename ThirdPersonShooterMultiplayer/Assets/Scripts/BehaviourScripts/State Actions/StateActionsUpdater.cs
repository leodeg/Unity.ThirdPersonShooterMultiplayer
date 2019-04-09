using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/State Actions Updater")]
    public class StateActionsUpdater : StateActions
    {
        public StateActions[] stateActions;

        public override void Execute (StateManager states)
        {
            if (stateActions != null)
            {
                for (int i = 0; i < stateActions.Length; i++)
                {
                    stateActions[i].Execute (states);
                }
            }
        }
    }
}