using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Initialize/InitClientController")]
    public class InitClientController : StateActions
    {
        public StateActions[] actions;

        public override void Execute (StateManager states)
        {
            states.transformInstance = states.transform;
            states.animatorInstance = states.transformInstance.GetComponentInChildren<Animator> ();
            states.rigidbodyInstance = states.transformInstance.GetComponent<Rigidbody> ();
            states.rigidbodyInstance.isKinematic = true;

            foreach (StateActions action in actions)
            {
                action.Execute (states);
            }
        }
    }
}