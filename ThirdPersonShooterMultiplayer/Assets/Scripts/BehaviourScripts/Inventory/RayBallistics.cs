using System;
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

            ClientShoot (states, weapon, direction, origin);

            Multiplayer.MultiplayerManager manager = Multiplayer.MultiplayerManager.Singleton;
            if (manager != null)
            {
                manager.BroadcastShootWeapon (states, direction, origin);
            }
        }

        public void ClientShoot (StateManager states, Weapon weapon, Vector3 direction, Vector3 origin)
        {
            RaycastHit[] hits = Physics.RaycastAll (origin, direction, raycastDistance, states.weaponRaycastLayers);
            if (hits == null || hits.Length == 0) return;

            RaycastHit closestHit = GetClosestRaycastHit (origin, hits, states.photonID);
            IHittable isHittable = closestHit.transform.GetComponentInParent<IHittable> ();
            CallOnHittableAction (states, weapon, direction, closestHit, isHittable);
        }

        public void LocalShoot (StateManager states, Weapon weapon, Vector3 direction, Vector3 origin)
        {
            Ray ray = new Ray (origin, direction);

            RaycastHit hit;
            Debug.DrawRay (origin, direction * raycastDistance, Color.red);
            if (Physics.Raycast (ray, out hit, raycastDistance, states.weaponRaycastLayers))
            {
                Debug.Log ("Hit name: " + hit.transform.name);
                IHittable isHittable = hit.transform.GetComponentInParent<IHittable> ();
                CallOnHittableAction (states, weapon, direction, hit, isHittable);
            }
        }

        private void CallOnHittableAction (StateManager states, Weapon weapon, Vector3 direction, RaycastHit closestHit, IHittable isHittable)
        {
            if (isHittable == null)
            {
                GameObject hitParticle = Managers.GameManagers.GetObjectPool ().GetObject (particleName);
                if (hitParticle != null)
                {
                    Quaternion rotation = Quaternion.LookRotation (-direction);
                    hitParticle.transform.position = closestHit.point;
                    hitParticle.transform.rotation = rotation;
                }
                else Debug.LogWarning ("RayBallistics::GameManagers::ObjectPool::ERROR::Hit particle is null or name of particle: [" + particleName + "] is wrong.");
            }
            else isHittable.OnHit (states, weapon, direction, closestHit.point);
        }

        public static RaycastHit GetClosestRaycastHit (Vector3 origin, RaycastHit[] hitList, int photonId)
        {
            int indexCounter = 0;
            int closestHitIndex = 0;
            float minDistance = float.MaxValue;

            foreach (RaycastHit hit in hitList)
            {
                float currentDistance = Vector3.Distance (origin, hit.point);
                if (currentDistance < minDistance)
                {
                    StateManager states = hit.transform.GetComponentInParent<StateManager> ();
                    if (states != null && states.photonID == photonId) continue;
                    minDistance = currentDistance;
                    closestHitIndex = indexCounter;
                }
                ++indexCounter;
            }
            return hitList[closestHitIndex];
        }
    }
}