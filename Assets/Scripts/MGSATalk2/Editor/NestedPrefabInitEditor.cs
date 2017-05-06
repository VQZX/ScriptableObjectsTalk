using UnityEditor;
using UnityEngine;

namespace MGSATalk2.Editor
{
    [CustomEditor(typeof(NestedPrefabInstantiator))]
    public class NestedPrefabInitEditor : UnityEditor.Editor
    {
        private NestedPrefabInstantiator npi;

        protected virtual void OnEnable()
        {
            npi = (NestedPrefabInstantiator) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Save Prefab structure")
            {
                SavePrefabState();
            }
        }

        private void SavePrefabState()
        {
            //first assign components to any children that have children
            int children = npi.transform.childCount;
            for (int i = 0; i < children; i++)
            {
                
            }
        }

        ////////////////////////////
        /// REFERENCE
        /// DataUtility.CreateAsset<LevelDefinition>(_level, levelData);
        // LevelDefinition asset = DataUtility.GetAsset<LevelDefinition>(levelData, typeof(LevelDefinition));
        ///////////
    }
}
