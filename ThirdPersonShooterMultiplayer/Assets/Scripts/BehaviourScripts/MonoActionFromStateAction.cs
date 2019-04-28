using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/MonoActionFromStateAction")]
    public class MonoActionFromStateAction : StateActions
    {
        public Action monoAction;

        public override void Execute (StateManager states)
        {
            monoAction.Execute ();
        }
    }
}