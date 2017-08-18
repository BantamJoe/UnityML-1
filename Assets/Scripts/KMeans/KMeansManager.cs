using UnityEngine;
using System.Collections.Generic;

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
        }

        void Start()
        {
            //Generate Data
            GenerateRandomData();

            //Create Classifier
            kMeans = new KMeans();
            kMeans.Fit(tempData);
            
            //Instantiate Shit
            CreateSpheres();

            CreateClusters();
        }

        public void GenerateRandomData()
        {
            tempData = new Matrix(3);
            int amount = Random.Range(100, 150);
            for (int i = 0; i < amount; i++)
            {
                double[] garbage = new double[3];
                for (int j = 0; j < garbage.Length; j++)
                {
                    garbage[j] = Random.Range(0, 20);
                }
                tempData.Add(garbage);
            }
        }

        public void CreateSpheres()
        {
            for(int i = 0; i < tempData.Count; i++)
            {
                Vector3 tempPos = new Vector3((float)tempData[i][0], (float)tempData[i][1], (float)tempData[i][2]);
                GameObject g = Instantiate(dataPointPrefab, tempPos, Quaternion.identity, this.transform);
                g.GetComponent<DataObject>().classType = kMeans.Clusters[i];
            }
        }

        public void CreateClusters()
        {
            for (int i = 0; i < kMeans.Centroids.Count; i++)
            {
                double[] cent = kMeans.Centroids[i];
                Vector3 tempPos = new Vector3((float)cent[0], (float)cent[1], (float)cent[2]);
                DataObject obj = Instantiate(centroidPrefab, tempPos, Quaternion.identity, this.transform).GetComponent<DataObject>();
                obj.classType = i;
            }

           // List<DataObject> spheres = new List<DataObject>(transform.GetComponentsInChildren<DataObject>());
        }
    }
}
