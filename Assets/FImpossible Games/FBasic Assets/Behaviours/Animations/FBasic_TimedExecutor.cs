using UnityEngine;
using UnityEngine.Events;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Simple component which is executing target event in choosed time period
    /// </summary>
    public class FBasic_TimedExecutor : MonoBehaviour
    {
        [Header("How much seconds should take space between executions")]
        public Vector2 RandomTimerRange = new Vector2(3f, 4f);

        public UnityEvent ToExecute;

        private float timer;

        private void Start()
        {
            ResetTimer();
        }

        private void Update()
        {
            timer -= Time.deltaTime;

            if ( timer <= 0f)
            {
                if (ToExecute != null) ToExecute.Invoke();
                ResetTimer();
            }
        }

        private void ResetTimer()
        {
            timer = Random.Range(RandomTimerRange.x, RandomTimerRange.y);
        }
    }
}
