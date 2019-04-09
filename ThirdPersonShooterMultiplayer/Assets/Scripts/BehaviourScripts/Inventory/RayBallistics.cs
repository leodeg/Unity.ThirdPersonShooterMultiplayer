using System.Collections;
using UnityEngine;

namespace StateAction
{
    [CreateAssetMenu (menuName = "Items/Ray Ballistics")]
    public class RayBallistics : Ballistics
    {
        public float raycastDistance = 100;
        public string particleName = "bullet_hit";

        public override void Execute (StateManager states, Weapon weapon)
        {
            Vector3 origin = weapon.properties.modelInstance.transform.position;
            Vector3 direction = states.movementProperties.aimPosition - origin;
            Ray ray = new Ray (origin, direction);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, raycastDistance, states.weaponRaycastLayers))
            {
                GameObject hitParticle = GameManagers.GetObjectPool ().GetObject (particleName);
                Quaternion rotation = Quaternion.LookRotation (-direction);
                hitParticle.transform.position = hit.point;
                hitParticle.transform.rotation = rotation;
            }
        }
    }
}