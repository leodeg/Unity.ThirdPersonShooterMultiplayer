using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Items/Ammo")]
    public class Ammo : ScriptableObject
    {
        public int carryingAmount;

        public virtual void OnHit ()
        {

        }
    }
}
