using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Items/Weapon")]
    public class Weapon : Item
    {
        public class WeaponProperties
        {
            public GameObject modelInstance;
            public WeaponController weaponController;
            public float lastFireTime;
        }

        [Header ("Weapon Properties")]
        public int currentBullets;
        public int maxBullets = 30;
        public float fireRate = 0.3f;
        public Ammo ammoType;

        [Header ("Right Hand Properties")]
        public StateObject.Vector3Variable rightHandPosition;
        public StateObject.Vector3Variable rightHandEulers;

        [Header ("Prefabs")]
        public GameObject modelPrefab;
        public WeaponProperties properties;

        [Header ("Animation Curves")]
        public AnimationCurve recoilY;
        public AnimationCurve recoilZ;

        [Header ("Ballistics")]
        public bool overrideBallistics;
        public Ballistics ballistics;

        public void Initialize ()
        {
            properties = new WeaponProperties ();
            properties.modelInstance = Instantiate (modelPrefab);
            properties.weaponController = properties.modelInstance.GetComponent<WeaponController> ();
            properties.weaponController.Initialization ();
            properties.lastFireTime = 0;

            ammoType = Managers.GameManagers.GetAmmoManager ().GetAmmo (ammoType.name);
        }

        public void Reload ()
        {
            int targetBulletsAmount = maxBullets;

            if (targetBulletsAmount > ammoType.carryingAmount)
                targetBulletsAmount = maxBullets - ammoType.carryingAmount;

            ammoType.carryingAmount -= targetBulletsAmount;
            currentBullets = targetBulletsAmount;
        }
    }
}