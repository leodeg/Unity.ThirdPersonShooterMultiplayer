using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Weapon/Shoot Action")]
    public class ShootAction : StateActions
    {
        public override void Execute (StateManager states)
        {
            if (states.currentState.isReloading)
            {
                if ((states.inventory.currentWeapon.currentBullets < states.inventory.currentWeapon.maxBullets)
                    && (states.inventory.currentWeapon.ammoType.carryingAmount > 0))
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
                else
                {
                    states.currentState.isReloading = false;
                }

                return;
            }

            if (states.currentState.isShooting && !states.currentState.isReloading)
            {
                states.currentState.isShooting = false;
                Weapon weapon = states.inventory.currentWeapon;

                if (weapon.currentBullets > 0)
                {
                    if (Time.realtimeSinceStartup - weapon.properties.lastFireTime > weapon.fireRate)
                    {
                        weapon.properties.lastFireTime = Time.realtimeSinceStartup;
                        weapon.properties.weaponController.Shoot ();
                        states.animatorController.StartRecoilAnimation ();

                        if (!weapon.overrideBallistics)
                        {
                            if (states.ballisticActions != null)
                            {
                                states.ballisticActions.Execute (states, weapon);
                            }
                        }
                        else
                        {
                            weapon.ballistics.Execute (states, weapon);
                        }

                        --weapon.currentBullets;
                        if (weapon.currentBullets < 0)
                        {
                            weapon.currentBullets = 0;
                        }
                    }
                }
                else
                {
                    states.currentState.isReloading = true;
                }
            }
        }
    }
}