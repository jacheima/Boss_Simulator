using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Simple class to play target animator state
    /// </summary>
    public class FBasic_AnimatorPlayState : MonoBehaviour
    {
        public string AnimationStateName = "Idle";
        public int AnimationLayer = 0;

        // v1.1
        [Tooltip("Normalized time so go from 0 to 1")]
        public Vector2 TimeOffset = Vector2.zero;

        void Start()
        {
            Animator anim = GetComponentInChildren<Animator>();

            if ( anim )
            {
                anim.Play(AnimationStateName, AnimationLayer,  Random.Range(TimeOffset.x, TimeOffset.y));
            }

            GameObject.Destroy(this);
        }
    }
}
