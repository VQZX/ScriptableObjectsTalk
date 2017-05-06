using UnityEngine;
using System.Collections;

namespace Flusk.Containers
{
    [System.Serializable]
    public struct Range
    {
        [SerializeField]
        private float min;
        [SerializeField]
        private float max;

        public float Min
        {
            get
            {
                return min;
            }
        }

        public float Max
        {
            get
            {
                return max;
            }
        }   

        public float Magnitude
        {
            get
            {
                return max - min;
            }
        }

        public Range(float min, float max )
        {
            this.max = System.Math.Max(min, max);
            this.min = System.Math.Min(min, max);
        }  

        public float ComparePoint ( float point )
        {
            float difference = point - min;
            float quotient = 0;
            if ( Magnitude != 0 )
            {
                quotient = difference / Magnitude;
            }
            else
            {
                quotient = Mathf.Infinity;
                Debug.LogError("I'd rather you didn't divide by zero");
            }
            return quotient;      
        }
    } 
}
