using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataObject : MonoBehaviour
{
    private Renderer _objectRenderer;

    public Color classColor;
    private int _classType;
    public int classType
    {
        get
        {
            return _classType;
        }
        set
        {
            _classType = value;
            setColor();
        }
    }
    public int indexInKMeans;

    protected Renderer objectRenderer
    {
        get
        {
            if (_objectRenderer == null)
            {
                _objectRenderer = GetComponentInChildren<Renderer>();
            }
            return _objectRenderer;
        }
    }
    
    public void setColor()
    {
        classColor = UnityML.KMeansManager.manager.classColors[classType];
        setColor(classColor);
    }

    public void setColor(Color color)
    {
        Debug.Log("CHANGE THE COLOR");
        objectRenderer.material.color = color;
    }
}
