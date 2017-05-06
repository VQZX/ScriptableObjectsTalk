#if UNITY_EDITOR
using UnityEditor; 
#endif
using System;
using UnityObject = UnityEngine.Object;

namespace Flusk.Utility
{
    public static class DataUtility
    {
        public struct AssetData
        {
            public string Title;
            public string Directory;
            public string DefaultName;
            public string Extension;
        }

        public static void CreateAsset<T>(T asset, AssetData data) where T : UnityObject
        {
#if UNITY_EDITOR
            string path = SaveFilePanel(data);
            bool isEmpty = String.IsNullOrEmpty(path);
            if (isEmpty)
            {
                return;
            }
            path = FileUtil.GetProjectRelativePath(path);
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets(); 
#endif
        }

        public static string SaveFilePanel( AssetData data )
        {
            string path = System.String.Empty;
#if UNITY_EDITOR
            path = EditorUtility.SaveFilePanel(data.Title, data.Directory, data.DefaultName, data.Extension); 
#endif
            return path;
        }

        public static T GetAsset<T>( AssetData data, Type type ) where T : UnityObject
        {
            T output = null;
#if UNITY_EDITOR
            string path = SaveFilePanel(data);
            path = FileUtil.GetProjectRelativePath(path);
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            output = asset;
#endif
            return output;
        }
    } 
}
