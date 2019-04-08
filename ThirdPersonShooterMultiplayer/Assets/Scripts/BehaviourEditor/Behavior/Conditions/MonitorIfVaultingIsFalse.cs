using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Conditions/Vaulting/Monitor If Vaulting Is False")]
    public class MonitorIfVaultingIsFalse : Condition
    {
        public override bool CheckCondition (StateManager state)
        {
            return !state.currentState.isVaulting;
        }
    }
}
