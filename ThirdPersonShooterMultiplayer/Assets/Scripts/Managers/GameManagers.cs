using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public static class GameManagers
    {
        private static ObjectPooler objectPooler;
        public static ObjectPooler GetObjectPool ()
        {
            if (objectPooler == null)
            {
                objectPooler = Resources.Load ("ObjectPooler") as ObjectPooler;
                objectPooler.Initialize ();
            }
            return objectPooler;
        }

        private static ResourcesManager resourcesManager;
        public static ResourcesManager GetResourcesManager ()
        {
            if (resourcesManager == null)
            {
                resourcesManager = Resources.Load ("ResourcesManager") as ResourcesManager;
                resourcesManager.Initialize ();
            }
            return resourcesManager;
        }

        private static AmmoManager ammoManager;
        public static AmmoManager GetAmmoManager ()
        {
            if (ammoManager == null)
            {
                ammoManager = Resources.Load ("AmmoManager") as AmmoManager;
                ammoManager.Initialize ();
            }
            return ammoManager;
        }
    }
}