using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/State Actions/ Load Weapon")]
    public class LoadWeapon : StateActions
    {
        public override void Execute (StateManager states)
        {
            ResourcesManager manager = GameManagers.GetResourcesManager ();
            Weapon weapon = manager.GetItemInstance (states.inventory.weaponID) as Weapon;
            states.inventory.currentWeapon = weapon;
            weapon.Initialize ();

            weapon.runtimeWeapon.modelInstance.transform.parent = states.animatorInstance.GetBoneTransform (HumanBodyBones.RightHand);
            weapon.runtimeWeapon.modelInstance.transform.localPosition = Vector3.zero;
            weapon.runtimeWeapon.modelInstance.transform.localEulerAngles = Vector3.zero;

            states.animatorHook.LoadWeapon (weapon);
        }
    }
}