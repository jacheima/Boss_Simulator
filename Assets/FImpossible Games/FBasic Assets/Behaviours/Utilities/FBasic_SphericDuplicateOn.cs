using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Class for duplicating objects on surface inside sphere drawed by this component's gizmos
    /// </summary>
    public class FBasic_SphericDuplicateOn : MonoBehaviour
    {
        public int RowCount = 32;
        public float Radius = 5f;
        public LayerMask LayerMask;

        public Vector2 fromRange = Vector2.zero;
        public Vector2 toRange = new Vector2(360, 360);

        [Range(0f,1f)]
        public float Randomize = 0f;
        public int Seed = 99999;
        private System.Random randomSeed;

        public GameObject ToDuplicate;
        public Vector3 RotationOffset;

        public Vector3 RandomRotationLocalAxis = Vector3.up;
        public Vector2 RandomRotationRange;

        public Transform AttachTo = null;
        public Transform AttachToNearestTransformOf = null;

        public List<GameObject> Generated;

        void OnDrawGizmos()
        {
            randomSeed = new System.Random(Seed);

            float step = 360f / (float)RowCount;

            Gizmos.color = new Color(0.2f, 0.9f, 0.4f, 0.8f);

            for (float x = 0; x < RowCount; x++)
            {
                if (x * step < fromRange.x) continue;
                if (x * step > toRange.x) continue;

                for (float y = 0; y < RowCount; y++)
                {
                    if (y * step < fromRange.y) continue;
                    if (y * step > toRange.y) continue;

                    Quaternion rotation = Quaternion.Euler(x * step, y * step, 0f);
                    Vector3 targetDir = rotation * -Vector3.forward;
                    if (Randomize != 0f) targetDir += RandomVectorSeed(-Randomize, Randomize);

                    Gizmos.DrawRay(transform.position + rotation * Vector3.forward * Radius, targetDir);
                }
            }
        }

        private Vector3 RandomVectorSeed(float rangeA, float rangeB)
        {
            return new Vector3(GetRandomRange( rangeA, rangeB), GetRandomRange(rangeA, rangeB), GetRandomRange(rangeA, rangeB));
        }

        private float GetRandomRange(float rangeA, float rangeB)
        {
            return rangeA + (float)randomSeed.NextDouble() * (rangeB + Mathf.Abs(rangeA));
        }

        public void Duplicate()
        {
            randomSeed = new System.Random(Seed);

            float step = 360f / (float)RowCount;

            GameObject container = new GameObject("Duplicated-" + ToDuplicate.name + "-Container");

            Transform[] transforms = null;
            if (AttachToNearestTransformOf) transforms = AttachToNearestTransformOf.GetComponentsInChildren<Transform>();

            for (float x = 0; x < RowCount; x++)
            {
                if (x * step < fromRange.x) continue;
                if (x * step > toRange.x) continue;

                for (float y = 0; y < RowCount; y++)
                {
                    if (y * step < fromRange.y) continue;
                    if (y * step > toRange.y) continue;

                    Quaternion rotation = Quaternion.Euler(x * step, y * step, 0f);

                    Vector3 targetDir = rotation * -Vector3.forward;
                    if (Randomize != 0f) targetDir += RandomVectorSeed(-Randomize, Randomize);

                    RaycastHit hit;
                    Ray ray = new Ray(transform.position + rotation * Vector3.forward * Radius, targetDir);
                    Physics.Raycast(ray, out hit, Radius, LayerMask);

                    if (hit.transform)
                    {
                        GameObject d = Instantiate(ToDuplicate);
                        d.transform.position = hit.point;
                        d.transform.rotation = Quaternion.LookRotation(hit.point + hit.normal - hit.point) * Quaternion.Euler(RotationOffset);
                        d.transform.rotation *= Quaternion.AngleAxis(Random.Range(RandomRotationRange.x, RandomRotationRange.y), RandomRotationLocalAxis);

                        if (transforms != null)
                        {
                            Transform nearest = transforms[0];
                            float nearestDist = Vector3.Distance(d.transform.position, nearest.position);

                            for (int i = 0; i < transforms.Length; i++)
                            {
                                float dist = Vector3.Distance(d.transform.position, transforms[i].position);
                                if (dist < nearestDist) nearest = transforms[i];
                            }

                            d.transform.SetParent(nearest, true);
                        }
                        else
                            d.transform.SetParent(container.transform, true);

                        Generated.Add(d);
                    }
                }
            }

            if (AttachTo)
                container.transform.SetParent(AttachTo, true);

            Generated.Add(container);
        }

        private void PurgeGenerated()
        {
            for (int i = Generated.Count - 1; i >= 0; i--)
            {
                if (Generated[i]) GameObject.DestroyImmediate(Generated[i]);
            }

            Generated.Clear();
        }


#if UNITY_EDITOR
        /// <summary>
        /// FM: Editor class component to enchance controll over component from inspector window
        /// </summary>
        [UnityEditor.CustomEditor(typeof(FBasic_SphericDuplicateOn))]
        public class FBasics_SphericDuplicateOnEditor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                FBasic_SphericDuplicateOn targetScript = (FBasic_SphericDuplicateOn)target;
                DrawDefaultInspector();

                GUILayout.Space(10f);

                if (GUILayout.Button("Duplicate")) targetScript.Duplicate();
                if (GUILayout.Button("Purge Generated")) targetScript.PurgeGenerated();
            }
        }


#endif

    }
}