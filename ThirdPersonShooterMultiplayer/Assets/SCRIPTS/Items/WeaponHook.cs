using System.Collections;
using UnityEngine;

namespace StateAction
{
    public class WeaponHook : MonoBehaviour
    {
        public Transform leftHandIkPosition;
        private ParticleSystem [] particles;
        private AudioSource audioSource;

        public void Initialization ()
        {
            GameObject audioObject = new GameObject ();
            audioObject.transform.parent = transform;

            audioSource = audioObject.AddComponent<AudioSource> ();
            audioSource.spatialBlend = 1;

            particles = GetComponentsInChildren<ParticleSystem> ();
        }

        public void Shoot ()
        {
            if (particles != null)
            {
                for (int i = 0; i < particles.Length; i++)
                {
                    particles [i].Play ();
                }
            }
        }
    }
}