using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Flusk.Utilities
{
	public static class GenericHandling
	{
	
		public static bool CheckNotNull<T> ( params T [] checkObjects )
		{
			bool isNotNull = false;
			for ( int i = 0; i < checkObjects.Length; i++ )
			{
				isNotNull = isNotNull || checkObjects[i] != null;
			}
			return isNotNull;
		}

		public static bool NotASingleNullNonAlloc<T> ( T [] checkObjects )
		{
			bool isNotNull = true;
			for ( int i = 0; i < checkObjects.Length; i++  )
			{
				isNotNull = isNotNull && ( checkObjects[i] != null );
			}
			return isNotNull;
		}

		public static bool NotASingleNull<T> ( params T [] checkObjects )
		{
			bool isNotNull = true;
			for ( int i = 0; i < checkObjects.Length; i++  )
			{
				isNotNull = isNotNull && ( checkObjects[i] != null );
			}
			return isNotNull;
		}

		public static void SwapItems<T> (ref T first, ref T second )
		{
			T temp = first;
			first = second;
			second = temp;
		}

		public static void LogItems<T>( T [] items )
		{
			for ( int i = 0; i < items.Length; i++ )
			{
				Debug.Log( items[i].ToString() );
			}
		}

        public static bool ValidIndex<T>(this T[] array, int index)
        {
            bool output;
            if ( index >= 0 && index < array.Length )
            {
                if ( array[index] != null )
                {
                    output = true;
                }  
                else
                {
                    output = false;
                } 
            }    
            else
            {
                output = false;
            }
            return output;
        }

        public static string ArrayItemToString<T> ( this T [] array )
        {
            string output = System.String.Empty;
            for ( int i = 0; i < array.Length; i++ )
            {
                output = System.String.Concat(output, " ", array[i]);
            }
            return output;
        }

        public static string [] ItemsToStringArray<T> ( this T [] array )
        {
            string[] outputs = new string[array.Length];
            for ( int i = 0; i < outputs.Length; ++i )
            {
                outputs[i] = array[i].ToString();
            }
            return outputs;
        }


        //TODO: shouldn't be here because this isn't generci
        public static float MaxSelectionFromArray<T> ( this float [] array, params int [] indices )
        {
            //List<float> selection = new List<float>();
            float currentMax = -Mathf.Infinity;
            for ( int i = 0; i < indices.Length; i++ )
            {
                int index = indices[i];
                
                if (index >= 0 && index < array.Length)
                {
                    float currentValue = array[index];
                    currentMax = Mathf.Max(currentMax, currentValue);
                }
            }
            return currentMax;
        }

        public static float MaxSelectionFromArray(this float[] array, int[] indices)
        {
            ///List<float> selection = new List<float>();
            float currentMax = -Mathf.Infinity;
            for (int i = 0; i < indices.Length; i++)
            {
                int index = indices[i];

                if (index >= 0 && index < array.Length)
                {
                    float currentValue = array[index];
                    currentMax = Mathf.Max(currentMax, currentValue);
                }
            }
            return currentMax;
        }
    }
}
