using UnityEngine;

namespace UnityML
{
    public class DataPoint : KMeansObject
    {
        private LineRenderer lineRenderer;
        private float counter;
        private float dist;

        public Transform origin
        {
            get
            {
                return transform;
            }
        }
        public Transform destination
        {
            get
            {
                return KMeansManager.manager.CentroidObjects[ClassType];
            }
        }

        public float lineDrawSpeed = 6f;

        public override void Awake()
        {
            base.Awake();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, origin.position);
        }

        private void Update()
        {
            dist = Vector3.Distance(origin.position, destination.position);
            if (counter < dist)
            {
                counter += .1f / lineDrawSpeed;
                float x = Mathf.Lerp(0, dist, counter);

                Vector3 a = origin.position;
                Vector3 b = destination.position;

                Vector3 pointAlongLine = x * Vector3.Normalize(b - a) + a;

                lineRenderer.SetPosition(1, pointAlongLine);
            }
        }

        public override void OnClassChanged()
        {
            base.OnClassChanged();
            //lineRenderer.material.color = CurrentClassColor;
        }
    }
}
