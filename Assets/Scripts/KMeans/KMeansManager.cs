using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace UnityML
{
    public class KMeansManager : MonoBehaviour
    {
        public static KMeansManager manager;
        public KMeans kMeans;
        public Matrix tempData;
        public GameObject centroidPrefab;
        public GameObject dataPointPrefab;
        public int numOfClusters = 3;
        public Color[] classColors = new Color[3];
        public bool objectsInstantiated = false;
        public List<Transform> CentroidObjects;
        public List<Transform> DataPointObjects;
        public int numberOfIterations = 10;

        void Awake()
        {
            if(manager == null)
            {
                manager = this;
            }
            else
            {
                Destroy(this);
            }
            CentroidObjects = new List<Transform>();
            DataPointObjects = new List<Transform>();
    }

        void Start()
        {
            //Generate Data
            GenerateRandomData();

            //Create Classifier
            kMeans = new KMeans(numOfClusters);
            kMeans.Fit(tempData);
            CreateSpheres();
            CreateClusters();

            StartCoroutine(startFit());
        }

        IEnumerator startFit()
        {
            for (int i = 0; i < numberOfIterations; i++)
            {
                yield return new WaitForSeconds(5.0f);
                kMeans.Next();
                UpdateSpheres();
            }
            
        }

        public void GenerateRandomData()
        {
            tempData = new Matrix(3);
            int amount = 1000;//Random.Range(100, 150);
            for (int i = 0; i < amount; i++)
            {
                double[] garbage = new double[3];
                for (int j = 0; j < garbage.Length; j++)
                {
                    garbage[j] = Random.Range(0, 200);
                }
                tempData.Add(garbage);
            }
        }

        public void CreateSpheres()
        {
            for(int i = 0; i < tempData.Count; i++)
            {
                Vector3 tempPos = new Vector3((float)tempData[i][0], (float)tempData[i][1], (float)tempData[i][2]);
                Transform g = Instantiate(dataPointPrefab, tempPos, Quaternion.identity, this.transform).GetComponent<Transform>();
                g.GetComponent<KMeansObject>().ClassType = kMeans.Clusters[i];
                DataPointObjects.Add(g);
            }
        }

        public void UpdateSpheres()
        {
            for(int i = 0; i < kMeans.Data.Count; i++)
            {
                DataPointObjects[i].GetComponent<KMeansObject>().ClassType = kMeans.Clusters[i];
            }
            for(int i = 0; i < kMeans.numOfClusters; i++)
            {
                CentroidObjects[i].position = new Vector3((float)kMeans.Centroids[i][0], (float)kMeans.Centroids[i][1], (float)kMeans.Centroids[i][2]);
            }
        }

        public void CreateClusters()
        {
            for (int i = 0; i < kMeans.Centroids.Count; i++)
            {
                double[] cent = kMeans.Centroids[i];
                Debug.Log(cent[0] + ", "+ cent[1] + ", " +cent[2]);
                Vector3 tempPos = new Vector3((float)cent[0], (float)cent[1], (float)cent[2]);
                Transform g = Instantiate(centroidPrefab, tempPos, Quaternion.identity, this.transform).GetComponent<Transform>();
                g.GetComponent<KMeansObject>().ClassType = i;
                CentroidObjects.Add(g);
            }

           // List<DataObject> spheres = new List<DataObject>(transform.GetComponentsInChildren<DataObject>());
        }

    }
}
