using UnityEngine;

namespace FIMSpace.Basics
{
    public class FBasic_Demo_Cannon : MonoBehaviour
    {
        [Header("How much seconds should take space between executions")]
        public Vector2 RandomTimerRange = new Vector2(3f, 4f);

        public GameObject BulletPrefab;
        public GameObject MuzzlePrefab;
        public Transform ShotPoint;

        private float timer;

        private void Start()
        {
            ResetTimer();
        }

        private void Update()
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                Shot();
                ResetTimer();
            }
        }

        public void Shot()
        {
            GameObject newBullet = Instantiate(BulletPrefab);
            newBullet.transform.position = ShotPoint.position;
            newBullet.transform.rotation = ShotPoint.rotation;

            GameObject muzzle = Instantiate(MuzzlePrefab);
            muzzle.transform.position = ShotPoint.position;
            muzzle.transform.rotation = ShotPoint.rotation;
        }

        private void ResetTimer()
        {
            timer = Random.Range(RandomTimerRange.x, RandomTimerRange.y);
        }
    }
}