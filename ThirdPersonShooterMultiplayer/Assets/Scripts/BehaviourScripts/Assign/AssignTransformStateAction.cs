using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Assign/AssignTransform")]
    public class AssignTransformStateAction : StateActions
    {
        public StateObject.TransformVariable transformVariable;

        public override void Execute (StateManager states)
        {
            transformVariable.transform = states.transformInstance;
        }
    }
}