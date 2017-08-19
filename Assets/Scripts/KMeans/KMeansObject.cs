using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityML
{
    public class KMeansObject : MonoBehaviour
    {
        public enum DataType
        {
            None,
            DataPoint,
            Centroid
        }

        private DataType _dataType;
        [SerializeField]
        private Renderer _objectRenderer;
        private Transform _myTransform;
        private Color _currentClassColor;
        private int _classType;
        private int _indexInKMeans;

        public DataType DType
        {
            get
            {
                return _dataType;
            }

            set
            {
                _dataType = value;
            }
        }

        public new Transform transform
        {
            get
            {
                if(_myTransform == null)
                {
                    _myTransform = GetComponent<Transform>();
                }

                return _myTransform;
            }
        }

        public Renderer ObjectRenderer
        {
            get
            {
                if (_objectRenderer == null)
                {
                    _objectRenderer = GetComponentInChildren<Renderer>();
                }
                return _objectRenderer;
            }

            set
            {
                _objectRenderer = value;
            }
        }

        public Color CurrentClassColor
        {
            get
            {
                return _currentClassColor;
            }

            set
            {
                _currentClassColor = value;
            }
        }

        public int ClassType
        {
            get
            {
                return _classType;
            }

            set
            {
                _classType = value;
                OnClassChanged();
            }
        }

        public int IndexInKMeans
        {
            get
            {
                return _indexInKMeans;
            }

            set
            {
                _indexInKMeans = value;
            }
        }

        public virtual void Awake()
        {
            DType = DataType.DataPoint;
        }

        public virtual void OnClassChanged()
        {
            CurrentClassColor = KMeansManager.manager.classColors[ClassType];
            UpdateColor();
        }

        void UpdateColor()
        {
            ObjectRenderer.material.color = CurrentClassColor;
        }
    }
}