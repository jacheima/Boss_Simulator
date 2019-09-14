using System;
using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Simple class to controll skeleton animations
    /// </summary>
    [System.Serializable] // Serializable for debug mode
    public class FBasic_FheelekAnimator
    {
        public bool AnimationHolder = false;

        protected FBasic_FheelekController controller;
        protected Animator animator;

        protected string lastAnimation = "";

        protected bool waitForIdle = false;
        protected float landingTimer = 0f;

        protected string defaultIdle = "Idle";
        protected string defaultRun = "Run";
        protected int locomotionLayer = 0;

        public FBasic_FheelekAnimator(FBasic_FheelekController contr)
        {
            controller = contr;
            animator = controller.GetComponent<Animator>();
        }

        /// <summary>
        /// Animator operations to animate fellek correctly
        /// </summary>
        internal void Animate(float acc)
        {
            // Using animator state infos as holders from launching other animations
            // But for this to work animation launched from PlayAnimationHoldUntilIdle() must direct to Idle state machine in animator window
            // I used it for attack animations for example, but in humanoid skeleton etc. it can be unusable
            if (AnimationHolder)
                if (waitForIdle)
                {
                    AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(locomotionLayer);
                    if (!nextInfo.IsName(lastAnimation))
                    {
                        if (animator.IsInTransition(locomotionLayer))
                            waitForIdle = false;
                        else
                        {
                            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(locomotionLayer);
                            if (!info.IsName(lastAnimation))
                                waitForIdle = false;
                        }
                    }

                    if (!waitForIdle) AnimationHolder = false;

                    return;
                }

            if (!AnimationHolder)
            {
                // Detecting change in beeing grounded to determinate landing moment
                //if (controller.Grounded == true && wasGrounded == false) Landing();
                // We need to detect ladning before object hit ground
                if (!controller.Grounded)
                {
                    if (controller.CharacterRigidbody.velocity.y < 0f)
                        if (Physics.Raycast(controller.transform.position, -controller.transform.up, 0.1f - controller.CharacterRigidbody.velocity.y * Time.fixedDeltaTime))
                        {
                            Landing();
                        }
                }

                landingTimer -= Time.deltaTime;
            }
            else
            // If we are holding animation (not looped one like landing) we checking it progress
            {
                if (!animator.IsInTransition(locomotionLayer))
                {
                    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(locomotionLayer);

                    if (stateInfo.normalizedTime > 0.7f)
                    {
                        AnimationHolder = false;
                    }
                }
            }

            // Controll over animations
            if (!AnimationHolder)
            {
                if (controller.Grounded)
                {
                    if (acc < -0.1f || acc > 0.1f)
                    {
                        CrossfadeTo(defaultRun, 0.25f, locomotionLayer);
                        LerpValue("AnimSpeed", (acc / controller.MaxSpeed) * 8f);
                    }
                    else
                    {
                        CrossfadeTo(defaultIdle, 0.25f, locomotionLayer);
                    }
                }
                else
                {
                    if (controller.CharacterRigidbody.velocity.y > 0f)
                    {
                        CrossfadeTo("Jump", 0.15f, locomotionLayer);
                    }
                    else
                    {
                        CrossfadeTo("Falling", 0.24f, locomotionLayer);
                    }
                }
            }
        }

        /// <summary>
        /// Crossfades to target animation and locks from launching other animations until state machine not turn back or be turning back to "Idle"
        /// </summary>
        public void PlayAnimationHoldUntilIdle(string animation, float crossfadeTime = 0.2f, int animationLayer = 0)
        {
            CrossfadeTo(animation, crossfadeTime, animationLayer);
            AnimationHolder = true;
            waitForIdle = true;
        }

        /// <summary>
        /// Setting default idle animation state name
        /// </summary>
        internal void SetDefaultIdle(string stateName)
        {
            defaultIdle = stateName;
        }

        /// <summary>
        /// Setting default run animation state name
        /// </summary>
        internal void SetDefaultRun(string stateName)
        {
            defaultRun = stateName;
        }

        /// <summary>
        /// Crossfading animation state to other and remember last crossfaded animation name for controll
        /// </summary>
        protected void CrossfadeTo(string animation, float time = 0.25f, int animationLayer = 0)
        {
            if (lastAnimation != animation)
            {
                animator.CrossFadeInFixedTime(animation, time, animationLayer);
                lastAnimation = animation;
            }
        }

        /// <summary>
        /// Executed when controller jumps succesfully
        /// </summary>
        public virtual void Jump()
        {
        }

        /// <summary>
        /// Animate landing animation if we hit ground hard enough
        /// </summary>
        protected virtual void Landing()
        {
            if (landingTimer > 0f) return;

            if (controller.CharacterRigidbody.velocity.y < -4.5f)
            {
                CrossfadeTo("Landing", 0.1f, locomotionLayer);
                AnimationHolder = true;
            }

            landingTimer = 0.5f;
        }

        /// <summary>
        /// Smooth changing parameter value in animator
        /// </summary>
        private void LerpValue(string parameter, float value)
        {
            FAnimatorMethods.LerpFloatValue(animator, parameter, value, 5f);
        }
    }
}
