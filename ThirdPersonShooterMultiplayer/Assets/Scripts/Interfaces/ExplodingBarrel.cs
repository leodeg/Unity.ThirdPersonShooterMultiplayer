using UnityEngine;
using System.Collections;

namespace StateAction
{
    public class ExplodingBarrel : MonoBehaviour, IHittable
    {
        public string particleName = "bullet_hit";

        public void OnHit (StateManager shooter, Weapon weapon, Vector3 direction, Vector3 position)
        {
            GameObject hitParticle = Managers.GameManagers.GetObjectPool ().GetObject (particleName);
            Quaternion rotation = Quaternion.LookRotation (-direction);
            hitParticle.transform.position = position;
            hitParticle.transform.rotation = rotation;
        }
    }
}