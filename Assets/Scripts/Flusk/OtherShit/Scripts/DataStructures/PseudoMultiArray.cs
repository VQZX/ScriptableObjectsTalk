using UnityEngine;
using System;
using UnityObject = UnityEngine.Object;

namespace Flusk.Containers2
{
    [Serializable]
    public struct Dimension
    {
        public int Order;
        public int Size;

        public Dimension ( int order, int size )
        {
            Order = order;
            Size = size;
        }
    }

    [Serializable]
    public class PseudoMultiArray<T>
    { 
        //That's crazy 0_0
        public T this [ params int [] indices ]
        {
            get
            {
                if ( indices.Length == dimensions.Length )
                {
                    int index = 0;
                    int length = indices.Length - 1;
                    for ( int i = 0; i < length; i++ )
                    {
                        int addition = dimensions[i].Size * indices[i];
                        index += addition;
                    }
                    index += indices[length];
                    return data[index];
                }
                else
                {
                    string message = string.Format("That is the incorrect amount of dimensions ({0}). The correct amount is {1}", indices.Length, count);
                    throw new FormatException(message);
                }
            }

            set
            {
                if (indices.Length == dimensions.Length)
                {
                    int index = 0;
                    int length = indices.Length - 1;
                    for (int i = 0; i < length; i++)
                    {
                        int addition = dimensions[i].Size * indices[i];
                        index += addition;
                    }
                    index += indices[length];
                    data[index] = value;
                }
                else
                {
                    string message = string.Format("That is the incorrect amount of dimensions ({0}). The correct amount is {1}", indices.Length, count);
                    throw new FormatException(message);
                }
            }
        }

        protected int count;
        public int Count
        {
            get
            {
                return count;
            }
        }

        [ReadOnly]
        [SerializeField]
        protected Dimension[] dimensions;

        [SerializeField]
        protected T[] data;

        /// <summary>
        /// Constriuctor, dimension data is defined by the length of the array being the dimensions and the individual values being the lengths
        /// </summary>
        /// <param name="dimensionData"></param>
        public PseudoMultiArray (params int[] dimensionData )
        {
            dimensions = new Dimension[dimensionData.Length];
            for (int i = 0; i < dimensionData.Length; i++ )
            {
                dimensions[i] = new Dimension(i, dimensionData[i]);
                count += dimensionData[i];
            }
            data = new T[count];
        }
    }

    [Serializable]
    public class SerializedMultiArray<T> : PseudoMultiArray<T> where T : UnityObject
    {
        public SerializedMultiArray( params int [] dimensionData ) : base ( dimensionData )
        {

        }
    }

    [Serializable]
    public class MultiArrayInt : PseudoMultiArray<int>
    {
        public MultiArrayInt(params int [] dimensionData ) : base( dimensionData )
        {

        }

    }
}
