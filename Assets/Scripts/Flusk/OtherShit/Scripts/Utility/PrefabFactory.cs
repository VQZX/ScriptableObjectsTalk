using UnityEngine;
using System.Collections;

namespace Flusk.PrefabUtility
{
    public class PrefabFactory : MonoBehaviour
    {
        public GameObject[] prefabs;

        private void Awake ()
        {
            for ( int i = 0; i < prefabs.Length; i++ )
            {
                GameObject temp = Instantiate<GameObject>(prefabs[i]);
                temp.transform.SetSiblingIndex(0);
            }
        }
    }

}