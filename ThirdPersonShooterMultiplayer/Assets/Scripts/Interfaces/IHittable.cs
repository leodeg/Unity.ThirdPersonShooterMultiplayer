using UnityEngine;

namespace StateAction
{
    public interface IHittable
    {
        void OnHit (StateManager shooter, Weapon weapon, Vector3 direction, Vector3 position);
    }
}