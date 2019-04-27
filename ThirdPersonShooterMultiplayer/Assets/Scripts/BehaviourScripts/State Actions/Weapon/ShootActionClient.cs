using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Weapon/ShootActionClient")]
    public class ShootActionClient : StateActions
    {
        public override void Execute (StateManager states)
        {
            if (states.currentState.isReloading)
            {
                if (!states.currentState.isInteracting)
                {
                    states.currentState.isInteracting = true;
                    states.PlayAnimation (states.animationName.rifleReloading);
                    states.animatorInstance.SetBool (states.animatorParameters.isInteracting, true);
                }
                else
                {
                    if (!states.animatorInstance.GetBool (states.animatorParameters.isInteracting))
                    {
                        states.currentState.isReloading = false;
                        states.currentState.isInteracting = false;
                        states.inventory.ReloadCurrentWeapon ();
                    }
                }
            }

            if (states.currentState.isShooting && !states.currentState.isReloading)
            {
                states.currentState.isShooting = false;
                Weapon weapon = states.inventory.currentWeapon;

                weapon.properties.weaponController.Shoot ();
                states.animatorController.StartRecoilAnimation ();
            }
        }

    }
}