using UnityEngine;
namespace UnityML
{
    public class Centroid : KMeansObject
    {
        public override void Awake()
        {
            DType = DataType.Centroid;
        }
    }
}
