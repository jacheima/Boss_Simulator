using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Component to move object with easing to target position with possibility to go back to initial position
    /// v1.1 rename from MoveObjectTo into SlideObjectTo
    /// </summary>
    public class FBasic_SlideObjectTo : MonoBehaviour
    {
        public FEasing.EFease EaseFunction = FEasing.EFease.EaseInOutCubic;

        [Tooltip("How long in seconds should be transition")]
        public float Duration = 2f;

        [Tooltip("Offset for target position in local space")]
        public Vector3 OffsetPosition = Vector3.up;

        [Tooltip("Offset for target rotation in local space")]
        public Vector3 OffsetRotation = Vector3.zero;

        /* Controll variables */
        protected bool goToTarget;
        protected float time;

        protected Vector3 initPos;
        protected Quaternion initRot;
        protected FEasing.Function func;


        private void Start()
        {
            initPos = transform.localPosition;
            initRot = transform.localRotation;

            goToTarget = false;
            enabled = false;
        }


        protected virtual void Update()
        {
            // Operating on easing methods depending on component's goToTarget flag
            if ( goToTarget )
            {
                time += Time.deltaTime;
                if (time >= Duration) enabled = false;
            }
            else
            {
                time -= Time.deltaTime;
                if (time <= 0f) enabled = false;
            }

            float easeValue = func(0f, 1f, time / Duration);
            transform.localPosition = Vector3.LerpUnclamped(initPos, initPos + OffsetPosition, easeValue);
            transform.localRotation = Quaternion.SlerpUnclamped(initRot, initRot * Quaternion.Euler(OffsetRotation), easeValue);
        }

        /// <summary>
        /// Switching transition animation
        /// </summary>
        public void ToggleMove()
        {
            goToTarget = !goToTarget;

            if (goToTarget) MoveToTarget(); else MoveBack();
        }

        /// <summary>
        /// Moves target object to desired position in component
        /// </summary>
        public void MoveToTarget()
        {
            goToTarget = true;
            enabled = true;
            func = FEasing.GetEasingFunction(EaseFunction);
        }

        /// <summary>
        /// Moves target object back to initial position and rotation
        /// </summary>
        public void MoveBack()
        {
            goToTarget = false;
            enabled = true;
            func = FEasing.GetEasingFunction(EaseFunction);
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Editor class for MoveObject component to have access to check animation from editor level (but in playmode)
    /// </summary>
    [UnityEditor.CustomEditor(typeof(FBasic_SlideObjectTo))]
    public class FBasic_MoveObjectToEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            FBasic_SlideObjectTo targetScript = (FBasic_SlideObjectTo)target;
            DrawDefaultInspector();

            GUILayout.Space(10f);

            if (GUILayout.Button("Switch Animation")) if (Application.isPlaying) targetScript.ToggleMove(); else Debug.Log("You must be in playmode to run this method!");
        }
    }
#endif

}
