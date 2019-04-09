using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Weapon/Load Weapon")]
    public class LoadWeapon : StateActions
    {
        public override void Execute (StateManager states)
        {
            ResourcesManager manager = GameManagers.GetResourcesManager ();
            Weapon weapon = manager.GetItemInstance (states.inventory.weaponID) as Weapon;
            states.inventory.currentWeapon = weapon;
            weapon.Initialize ();

            weapon.properties.modelInstance.transform.parent = states.animatorInstance.GetBoneTransform (HumanBodyBones.RightHand);
            weapon.properties.modelInstance.transform.localPosition = states.animatorInstance.GetBoneTransform (HumanBodyBones.RightHand).localPosition;
            weapon.properties.modelInstance.transform.localEulerAngles = weapon.rightHandEulers.value;

            states.animatorController.LoadWeapon (weapon);
        }
    }
}