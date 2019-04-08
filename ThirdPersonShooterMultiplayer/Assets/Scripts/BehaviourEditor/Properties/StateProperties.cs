using UnityEngine;

namespace StateAction
{
	[System.Serializable]
	public class StateProperties
	{
		public bool isAiming;
        public bool isInteracting;
        public bool isShooting;
        public bool isReloading;
        public bool isCrouching;
        public bool isVaulting;

        public void SetCrouching ()
        {
            isCrouching = !isCrouching;
        }

        public void SetReloading ()
        {
            isReloading = true;
        }
    }
}