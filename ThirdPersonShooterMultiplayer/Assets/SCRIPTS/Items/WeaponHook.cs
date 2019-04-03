using System.Collections;
using UnityEngine;

namespace StateAction
{
    public class WeaponHook : MonoBehaviour
    {
        [Header ("IK")]
        public Transform leftHandIkPosition;

        [Header ("Slider")]
        public Transform slider;
        public AnimationCurve sliderCurve;
        public float sliderSpeed = 6;
        public float multiplier = 1;

        private Vector3 startPosition;
        private float sliderTime;

        private bool isShooting;
        private bool initSliderLerp;

        private ParticleSystem [] particles;
        private AudioSource audioSource;

        public void Initialization ()
        {
            GameObject audioObject = new GameObject ();
            audioObject.name = "AudioObject";
            audioObject.transform.parent = transform;

            audioSource = audioObject.AddComponent<AudioSource> ();
            audioSource.spatialBlend = 1;

            particles = GetComponentsInChildren<ParticleSystem> ();

            if (slider != null)
            {
                startPosition = slider.localPosition;
            }
        }

        private void Update ()
        {
            if (isShooting)
            {
                UpdateSlider ();
            }
        }

        private void UpdateSlider ()
        {
            if (!initSliderLerp)
            {
                initSliderLerp = true;
                sliderTime = 0;
            }

            sliderTime += Time.deltaTime * sliderSpeed;
            if (sliderTime > 1) ResetSlider ();
            UpdateSliderPosition ();
        }

        private void UpdateSliderPosition ()
        {
            Vector3 targetPosition = startPosition;
            targetPosition.x = sliderCurve.Evaluate (sliderTime) * multiplier;
            slider.transform.localPosition = targetPosition;
        }

        private void ResetSlider ()
        {
            sliderTime = 1;
            initSliderLerp = false;
            isShooting = false;
        }

        public void Shoot ()
        {
            isShooting = true;

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