using System;
using System.Collections;
using UnityEngine;

namespace StateAction
{
    [System.Serializable]
    public class Inventory
    {
        public string weaponID;
        public Weapon currentWeapon;

        public void ReloadCurrentWeapon ()
        {
            currentWeapon.Reload ();
        }
    }
}