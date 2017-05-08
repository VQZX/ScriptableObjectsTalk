using Flusk.Utility;
using MGSATalk2.Data;
using UnityEditor;
using UnityEngine;

namespace MGSATalk2
{
    [DisallowMultipleComponent]
    public class NestedPrefabInstantiator : MonoBehaviour
    {
        [SerializeField] protected NestedPrefab nest;
        private bool initialized = false;

        //private string DEFAULT_NAME = "Prefab";

        public void Config()
        {
            initialized = true;
            if (initialized)
            {
                
            }
            int children = transform.childCount;
            for (int i = 0; i < children; i++)
            {
                var current = transform.GetChild(i);
                var npi = current.gameObject.AddComponent<NestedPrefabInstantiator>();
                npi.Config();
                //string fileTitle = string.Format("{0}_{1}", gameObject.name, DEFAULT_NAME);
                //nest = DataUtility.GetAsset<NestedPrefab>(fileTitle);
            }
        }

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
