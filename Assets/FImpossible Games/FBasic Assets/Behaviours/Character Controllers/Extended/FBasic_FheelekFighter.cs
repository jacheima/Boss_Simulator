using FIMSpace.Basics;
using UnityEngine;

namespace FIMSpace.Audio
{
    /// <summary>
    /// FM: Derived from FBasic_FheelekController to add some figting features
    /// </summary>
    public class FBasic_FheelekFighter : FBasic_FheelekController
    {
        // Not recommend to do audio things like that, but this time it's for presentation
        private AudioSource HitSource;
        public AudioClip SwingAudioClip;

        protected override void Start()
        {
            base.Start();
            fheelekAnimator.SetDefaultIdle("Idle Hammer");
            fheelekAnimator.SetDefaultRun("Idle Hammer");

            HitSource = gameObject.GetComponent<AudioSource>();
        }

        protected override void Update()
        {
            base.Update();

            if ( Input.GetMouseButtonDown(0))
            {
                fheelekAnimator.PlayAnimationHoldUntilIdle("Attack Hammer", 0.15f);
            }
        }

        // Animator Events \\

        public void ESwing()
        {
            HitSource.PlayOneShot(SwingAudioClip, 0.9f);
        }

        public void EHit()
        {
            //HitSource.Play();
        }
    }

}