using System.Collections.Generic;
using UnityEngine;

namespace MGSATalk2.Data
{
    public class NestedPrefab : ScriptableObject
    {
        [SerializeField]
        protected GameObject[] children;

        public GameObject this[int i]
        {
            get { return children[i]; }
        }

        public int Count
        {
            get
            {
                if (children == null)
                {
                    return 0;
                }
                return children.Length;
            }
        }

    }
}
