using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

//TODO: Do checks for same dimensionality
//TODO: Make with generics later
public class KMeans {
    int _numOfClusters;
    Matrix _data;
    Matrix _centroids;
    int[] _clusters;

    public KMeans(int numOfClust=2)
    {
        numOfClusters = numOfClust;
    }

    public int numOfClusters
    {
        get
        {
            return _numOfClusters;
        }
        protected set
        {
            _numOfClusters = value;
        }
    }

    public Matrix Data
    {
        get
        {
            return _data;
        }

        set
        {
            _data = value;
        }
    }

    /// <summary>
    /// The center points of our clusters
    /// </summary>
    public Matrix Centroids
    {
        get
        {
            if(_centroids == null)
            {
                _centroids = new Matrix(Data.Dimension);
            }
            return _centroids;
        }

        set
        {
            _centroids = value;
        }
    }
    
    /// <summary>
    /// The Clusters for our data
    /// </summary>
    public int[] Clusters
    {
        get
        {
            if(_clusters == null)
            {
                _clusters = new int[Data.Count];
            }
            return _clusters;
        }
    }

    public void Fit(Matrix data, bool generateCentroids = true)
    {
        Data = data;

        if (generateCentroids)
        {
            //generate our own centroids k times
            setCentroids();
        }

        FindClusters();
    }

    public void Next()
    {
        FindClusters();
        UpdateCentroidPositions();
    }

    public void UpdateCentroidPositions()
    {
        int[] totals = new int[numOfClusters];
        Matrix averagedVectors = new Matrix(Centroids.Dimension);

        for (int i = 0; i < numOfClusters; i++)
        {
            averagedVectors.Add(new double[Centroids.Dimension]);
        }

        for(int i = 0; i < numOfClusters; i++)
        {
            for (int j = 0; j < Data.Count; j++)
            {
                if (Clusters[j] == i)
                {
                    averagedVectors[i] = VectorSum(averagedVectors[i], Data[j]);
                    totals[i] += 1;
                }
            }
            double[] avg = ScaleVector(averagedVectors[i], (1.0f / totals[i]));
            Centroids[i] = avg;
        }

        Debug.Log(averagedVectors.ToString());
    }

    public double[] ScaleVector(double[] vector, float scaler)
    {
        double[] newVector = new double[vector.Length];
        for (int i = 0; i < Centroids.Dimension; i++)
        {
            newVector[i] = vector[i] * scaler;
        }
        return newVector;
    }

    public Matrix SelectMatrix(int selection)
    {
        Matrix m = new Matrix(Data.Dimension);
        for (int i = 0; i < Data.Count; i++)
        {
            if (Clusters[i] == i)
            {
                m.Add(Data[i]);
            }
        }
        return m;
    }

    /// <summary>
    /// Returns a double d
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public double[] VectorSum(double[] a, double[] b)
    {
        if(a.Length != b.Length)
        {
            throw new Exception("[KMEANS]: You fucked up");
        }
        double[] d = new double[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            d[i] = a[i] + b[i];
        }
        return d;
    }

    public void FindClusters()
    {
        for(int i = 0; i < Data.Count; i++)
        {
            int closestCluster = GetClosestCluster(Data[i]);
            Clusters[i] = closestCluster;
        }
    }

    public static double Distance(double[] x1, double[] x2)
    {
        if(x1.Length != x2.Length)
        {
            throw new Exception("[KMEANS]: Not same length");
        }
        double total = 0;
        for(int i = 0; i < x1.Length; i++)
        {
            total += ((x1[i] - x2[i]) * (x1[i] - x2[i]));
        }
        return Math.Sqrt(total);
    }

    private int GetClosestCluster(double[] dataPoint)
    {
        double smallestDist = double.MaxValue;
        int closestCluster = -1;
        for (int j = 0; j < Centroids.Count; j++)
        {
            double dist = Distance(dataPoint, Centroids[j]);
            if (dist < smallestDist)
            {
                smallestDist = dist;
                closestCluster = j;
            }
        }
        return closestCluster;
    }

    public void setCentroids()
    {
        for(int i = 0; i < numOfClusters; i++)
        {
            setCentroid();
        }
    }

    public void setCentroid()
    {
        //Random rand = new Random();
        
        double[] c = new double[Data.Dimension];
        for(int i = 0; i < Data.Dimension; i++)
        {
            //c[i] = rand.NextDouble()*(Data.MAX - Data.MIN) + Data.MIN;
            c[i] = UnityEngine.Random.Range((float)Data.MIN, (float)Data.MAX);
        }
        setCentroid(c);
    }

    public void setCentroid(double[] cents)
    {
        if(cents.Length != Centroids.Dimension)
        {
            throw new System.IndexOutOfRangeException("[KMEANS]: SET CENTROID HAS INVALID DIMENSION");
        }
        Centroids.Add(cents);
    }

}
