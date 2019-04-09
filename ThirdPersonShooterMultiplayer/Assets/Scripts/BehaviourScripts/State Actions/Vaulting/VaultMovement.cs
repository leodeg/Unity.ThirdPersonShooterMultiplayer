using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Vaulting/Vault Movement")]
    public class VaultMovement : StateActions
    {
        public override void Execute (StateManager states)
        {
            VaultData vaultData = states.vaultData;
            vaultData.vaultTime += states.deltaTime * vaultData.vaultSpeed;

            if (!vaultData.isInitialized)
            {
                vaultData.vaultTime = 0;
            }

            if (vaultData.vaultTime > 1)
            {
                vaultData.isInitialized = false;
                states.currentState.isVaulting = false;
            }

            states.transformInstance.position = Vector3.Lerp (vaultData.startPosition, vaultData.endingPosition, vaultData.vaultTime); ;
        }
    }
}