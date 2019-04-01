using UnityEngine;
using System.Collections;

namespace StateAction
{
    public static class GameManagers
    {
        static ResourcesManager resourcesManager;

        public static ResourcesManager GetResourcesManager ()
        {
            if (resourcesManager == null)
            {
                resourcesManager = Resources.Load ("ResourcesManager") as ResourcesManager;
                resourcesManager.Initialize ();
            }
            return resourcesManager;
        }
    }
}