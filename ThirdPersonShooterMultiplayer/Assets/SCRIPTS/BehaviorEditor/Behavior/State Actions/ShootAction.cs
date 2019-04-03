using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/State Actions/Shoot Action")]
    public class ShootAction : StateActions
    {
        public override void Execute (StateManager states)
        {
            if (states.stateProperties.isShooting)
            {
                states.stateProperties.isShooting = false;
                Weapon weapon = states.inventory.currentWeapon;
                if (weapon.currentBullets > 0)
                {
                    if (Time.realtimeSinceStartup - weapon.runtimeWeapon.lastFired > weapon.fireRate)
                    {
                        weapon.runtimeWeapon.lastFired = Time.realtimeSinceStartup;
                        weapon.runtimeWeapon.weaponHook.Shoot ();
                        states.animatorHook.RecoilActual ();

                        --weapon.currentBullets;
                        if (weapon.currentBullets < 0)
                            weapon.currentBullets -= 0;
                    }
                }
            }
        }
    }
}