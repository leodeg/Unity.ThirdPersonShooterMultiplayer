using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Initialize/InitPlayerRagdoll")]
    public class InitPlayerRagdoll : StateActions
    {
        public bool enableRagdoll;

        public override void Execute (StateManager states)
        {
            if (enableRagdoll)
            {
                for (int i = 0; i < states.ragdollRigidbodies.Count; i++)
                {
                    states.ragdollColliders[i].isTrigger = false;
                    states.ragdollRigidbodies[i].isKinematic = false;
                }
            }
            else
            {
                Rigidbody[] rigidbodies = states.transformInstance.GetComponentsInChildren<Rigidbody> ();

                if (rigidbodies == null || rigidbodies.Length == 0)
                {
                    Debug.LogWarning ("InitPlayerRagdoll::ERROR::Ragdolls rigidbodies is not found!");
                    return;
                }

                foreach (Rigidbody body in rigidbodies)
                {
                    if (body == states.rigidbodyInstance) continue;

                    states.ragdollRigidbodies.Add (body);
                    body.isKinematic = true;

                    Collider collider = body.GetComponent<Collider> ();
                    states.ragdollColliders.Add (collider);
                    collider.isTrigger = true;
                }
            }
        }
    }
}