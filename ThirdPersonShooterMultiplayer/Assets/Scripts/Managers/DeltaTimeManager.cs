using UnityEngine;
using System.Collections;

namespace Managers
{
    [CreateAssetMenu (menuName = "Managers/DeltaTimeManager")]
    public class DeltaTimeManager : StateAction.Action
    {
        public bool useFixedDeltaTime;
        public StateObject.FloatVariable variable;

        public override void Execute ()
        {
            variable.value = (useFixedDeltaTime) ? Time.fixedDeltaTime : Time.deltaTime;
        }
    }
}