using System;
using System.Collections;
using UnityEngine;

namespace StateAction
{
    public static class GameManagers
    {
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