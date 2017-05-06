using Flusk.Utility;
using MGSATalk2.Data;
using UnityEditor;
using UnityEngine;

namespace MGSATalk2
{
    public class NestedPrefabInstantiator : MonoBehaviour
    {
        [SerializeField] protected NestedPrefab nest;

        private bool initialized = false;

        public void Generate()
        {
            Init();
        }

        protected virtual void Awake()
        {
            Init();
        }

        private void Init()
        {
            int count = nest.Count;
            for (int i = 0; i < nest.Count; ++i)
            {
                var current = nest[i];
                GameObject copy = (GameObject) PrefabUtility.InstantiatePrefab(current);
                copy.transform.SetParent(transform);
                var nested = copy.GetComponent<NestedPrefabInstantiator>();
                if (nested == null)
                {
                    return;
                }
                nested.Generate();
            }
        }
    }
}
