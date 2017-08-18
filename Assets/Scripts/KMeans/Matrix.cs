using System;
using System.Collections.Generic;

//TODO: check if list is jagged
//TODO: implement Enumerator
public class Matrix { 
    List<double[]> _matrix;
    int dimension;
    double _min;
    double _max;
    bool changeInMatrix = true;

    public List<double[]> matrix
    {
        get
        {
            return _matrix;
        }
        set
        {
            _matrix = value;
        }
    }

    public int Count
    {
        get
        {
            return matrix.Count;
        }
    }

    public double MAX
    {
        get
        {
            if (changeInMatrix)
            {
                getMinAndMax();
            }
            return _max;
        }
        protected set
        {
            _max = value;
        }
    }

    public double MIN
    {
        get
        {
            if (changeInMatrix)
            {
                getMinAndMax();
            }
            return _min;
        }
        protected set
        {
            _min = value;
        }
    }


    void getMinAndMax()
    {
        double currentMin = matrix[0][0];
        double currentMax = matrix[0][0];
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                if (matrix[i][j] > currentMax)
                {
                    currentMax = matrix[i][j];
                }
                else if (matrix[i][j] < currentMin)
                {
                    currentMin = matrix[i][j];
                }
            }
        }
        changeInMatrix = false;
        MAX = currentMax;
        MIN = currentMin;
    }
    
    
    public int Dimension
    {
        get
        {
            return dimension;
        }

        set
        {
            dimension = value;
        }
    }
    

    public double[] this[int i]
    {
        get
        {
            if (i < 0 || i > matrix.Count)
            {
                throw new System.IndexOutOfRangeException("[MATRIX]: Index out of range");
            }
            return matrix[i];
        }
        set
        {
            matrix[i] = value;
        }
    }

    public double this[int i, int j]
    {
        get
        {
            if (i < 0 || i > matrix.Count)
            {
                throw new System.IndexOutOfRangeException("[MATRIX]: Index out of range");
            }
            else if (j < 0 || i > Dimension)
            {
                throw new System.IndexOutOfRangeException("[MATRIX]: Item is larger than array dimension");
            }
            return matrix[i][j];
        }
        set
        {
            matrix[i][j] = value;
        }
    }

    public Matrix(int dim)
    {
        Dimension = dim;
        matrix = new List<double[]>();
    }

    public Matrix(List<double[]> list)
    {
        //look at TODO
        matrix = list;
    }
    
    //TODO: Check added value against max and min to see if need change
    public void Add(double[] value)
    {
        if(value.Length != Dimension)
        {
            throw new System.Exception("[MATRIX]: Added Invalid Dimension");
        }
        matrix.Add(value);
        changeInMatrix = true;
    }

    public void Remove(double[] value)
    {
        matrix.Remove(value);
        changeInMatrix = true;
    }

    public void Remove(int value)
    {
        matrix.RemoveAt(value);
        changeInMatrix = true;
    }
}
