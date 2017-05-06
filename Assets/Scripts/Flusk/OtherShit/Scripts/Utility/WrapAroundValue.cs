using UnityEngine;
using System;

namespace Flusk.Utility
{
    [Serializable]
    public class WrapAroundValue
    {
        [SerializeField]
        private float wrapValue;

        public float Value
        {
            get
            {
                return wrapValue;
            }

            set
            {
                if ( value < min )
                {
                    wrapValue = max;
                    if ( OnWrapUp != null )
                    {
                        OnWrapUp();
                    }
                }
                else if ( value > max )
                {
                    wrapValue = min;
                    if ( OnWrapDown != null )
                    {
                        OnWrapDown();
                    }
                }
                else
                {
                    wrapValue = value;
                }
            }
        }

        public float Min
        {
            get
            {
                return min;
            }

            set
            {
                min = value;
            }
        }

        public float Max
        {
            get
            {
                return max;
            }

            set
            {
                max = value;
            }
        }

        private float min;
        private float max;

        private Action OnWrapUp;
        private Action OnWrapDown;

        public WrapAroundValue (float min, float max )
        {
            this.min = min;
            this.max = max;
        }
    } 
}
