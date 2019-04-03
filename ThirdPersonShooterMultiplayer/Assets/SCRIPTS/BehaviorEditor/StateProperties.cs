using UnityEngine;

namespace StateAction
{
	[System.Serializable]
	public class StateProperties
	{
		public bool isAiming;
        public bool isInteracting;
        public bool isShooting;
        public bool isCrouching;

        public void SetCrouching ()
        {
            isCrouching = !isCrouching;
        }
    }
}