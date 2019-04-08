using UnityEngine;
using System.Collections;

namespace StateAction
{
    [System.Serializable]
    public class VaultData
    {
        public Vector3 startPosition;
        public Vector3 endingPosition;
        public float vaultSpeed = 2;

        public float animationLength;
        public float vaultTime;
        public bool isInitialized;
    }
}