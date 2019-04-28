using UnityEngine;
using System.Collections;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Actions/Stats/ObservePlayerStats")]
    public class ObservePlayerStats : Action
    {
        [Header ("States")]
        public StateObject.StateVariable states;

        [Header ("Health")]
        public StateObject.GameEvent healthIsChanged;
        public StateObject.IntVariable playerHealth;

        [Header ("Ammo")]
        public StateObject.GameEvent ammoIsChanged;
        public StateObject.IntVariable currentAmmo;


        public override void Execute ()
        {
            if (states.value == null)
            {
                Debug.LogError ("ObservePlayerStats::ERROR::State variable is null.");
                return;
            }

            if (states.value.currentState.healthChangeFlag)
            {
                states.value.currentState.healthChangeFlag = false;
                playerHealth.value = states.value.playerStats.health;
                healthIsChanged.Raise ();
            }

            if (currentAmmo.value != states.value.inventory.currentWeapon.currentBullets)
            {
                currentAmmo.value = states.value.inventory.currentWeapon.currentBullets;
                ammoIsChanged.Raise ();
            }
        }
    }
}
