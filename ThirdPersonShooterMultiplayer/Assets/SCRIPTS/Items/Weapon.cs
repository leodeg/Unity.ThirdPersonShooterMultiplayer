using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Items/Weapon")]
    public class Weapon : Item
    {
        public class RuntimeWeapon
        {
            public GameObject modelInstance;
            public WeaponHook weaponHook;
            public float lastFired;

        }

        [Header ("Weapon Properties")]
        public int currentBullets;
        public int magazinesBullets = 30;
        public float fireRate = 0.3f;


        public StateObject.Vector3Variable rightHandPosition;
        public StateObject.Vector3Variable rightHandEulers;
        public GameObject modelPrefab;
        public RuntimeWeapon runtimeWeapon;

        public AnimationCurve recoilY;
        public AnimationCurve recoilZ;

        public void Initialize ()
        {
            runtimeWeapon = new RuntimeWeapon ();
            runtimeWeapon.modelInstance = Instantiate (modelPrefab);
            runtimeWeapon.weaponHook = runtimeWeapon.modelInstance.GetComponent<WeaponHook> ();
            runtimeWeapon.weaponHook.Initialization ();
            runtimeWeapon.lastFired = 0;
        }
    }
}