using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StateAction;

namespace Managers
{
    [CreateAssetMenu (menuName = "Managers/Ammo Manager")]
    public class AmmoManager : ScriptableObject
    {
        public List<Ammo> ammunitions = new List<Ammo> ();
        Dictionary<string, Ammo> ammunitionsDictionary = new Dictionary<string, Ammo> ();

        public void Initialize ()
        {
            for (int i = 0; i < ammunitions.Count; i++)
            {
                Ammo ammunition = Instantiate (ammunitions[i]);
                ammunition.name = ammunitions[i].name;
                ammunitionsDictionary.Add (ammunition.name, ammunition);
            }
        }

        public Ammo GetAmmo (string ammoName)
        {
            Ammo ammo = null;
            ammunitionsDictionary.TryGetValue (ammoName, out ammo);
            return ammo;
        }
    }
}