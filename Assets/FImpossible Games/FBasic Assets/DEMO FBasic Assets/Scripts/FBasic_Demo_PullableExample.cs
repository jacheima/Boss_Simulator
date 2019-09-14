using UnityEngine;

namespace FIMSpace.Basics
{
    public class FBasic_Demo_PullableExample : MonoBehaviour
    {
        public FBasic_Pullable TargetPullable;
        public FBasic_Rotator TargetRotator;

        void Update()
        {
            TargetRotator.RotationSpeed = TargetPullable.YValue * 10f;
        }
    }
}