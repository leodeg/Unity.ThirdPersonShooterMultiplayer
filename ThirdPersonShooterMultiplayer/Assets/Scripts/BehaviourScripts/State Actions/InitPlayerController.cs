using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Initialize/InitPlayerController")]
    public class InitPlayerController : StateActions
    {
        public StateActions updater;

        public override void Execute (StateManager states)
        {
            if (states.setupDefaultLayerAtStart)
            {
                states.groundLayers = ~(1 << 9 | 1 << 3);
            }

            states.transformInstance = states.transform;
            states.animatorInstance = states.transformInstance.GetComponentInChildren<Animator> ();
            states.rigidbodyInstance = states.transformInstance.GetComponent<Rigidbody> ();

            states.rigidbodyInstance.drag = 4;
            states.rigidbodyInstance.angularDrag = 999;
            states.rigidbodyInstance.constraints = RigidbodyConstraints.FreezeRotation;

            if (updater != null)
                updater.Execute (states);
        }
    }
}