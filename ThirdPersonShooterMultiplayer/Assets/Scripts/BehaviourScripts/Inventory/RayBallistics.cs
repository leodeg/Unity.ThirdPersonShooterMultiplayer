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
            Debug.DrawRay (origin, direction * raycastDistance, Color.red);
            if (Physics.Raycast (ray, out hit, raycastDistance, states.weaponRaycastLayers))
            {
                Debug.Log ("Hit name: " + hit.transform.name);
                IHittable isHittable = hit.transform.GetComponentInParent<IHittable> ();

                if (isHittable == null)
                {
                    GameObject hitParticle = Managers.GameManagers.GetObjectPool ().GetObject (particleName);
                    if (hitParticle != null)
                    {
                        Quaternion rotation = Quaternion.LookRotation (-direction);
                        hitParticle.transform.position = hit.point;
                        hitParticle.transform.rotation = rotation;
                    }
                    else
                    {
                        Debug.LogWarning ("RayBallistics::ERROR::Hit particle is null.");
                    }
                }
                else
                {
                    isHittable.OnHit (states, weapon, direction, hit.point);
                }
            }

            Multiplayer.MultiplayerManager manager = Multiplayer.MultiplayerManager.Singleton;
            if (manager != null)
            {
                manager.BroadcastShootWeapon (states, direction, origin);
            }
        }

        public void ClientShoot (StateManager states, Vector3 direction, Vector3 origin)
        {
            Ray ray = new Ray (origin, direction);

            RaycastHit hit;
            Debug.DrawRay (origin, direction * raycastDistance, Color.red);
            if (Physics.Raycast (ray, out hit, raycastDistance, states.weaponRaycastLayers))
            {
                Debug.Log ("Hit name: " + hit.transform.name);
                IHittable isHittable = hit.transform.GetComponentInParent<IHittable> ();

                if (isHittable == null)
                {
                    GameObject hitParticle = Managers.GameManagers.GetObjectPool ().GetObject (particleName);
                    if (hitParticle != null)
                    {
                        Quaternion rotation = Quaternion.LookRotation (-direction);
                        hitParticle.transform.position = hit.point;
                        hitParticle.transform.rotation = rotation;
                    }
                    else
                    {
                        Debug.LogWarning ("RayBallistics::ERROR::Hit particle is null.");
                    }
                }
                else
                {
                    isHittable.OnHit (states, states.inventory.currentWeapon, direction, hit.point);
                }
            }
        }
    }
}