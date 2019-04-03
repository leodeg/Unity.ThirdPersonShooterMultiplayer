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
            weapon.runtimeWeapon.modelInstance.transform.localPosition = states.animatorInstance.GetBoneTransform (HumanBodyBones.RightHand).localPosition;
            weapon.runtimeWeapon.modelInstance.transform.localEulerAngles = weapon.rightHandEulers.value;

            states.animatorHook.LoadWeapon (weapon);
        }
    }
}