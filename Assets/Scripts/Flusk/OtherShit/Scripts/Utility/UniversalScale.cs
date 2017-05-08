using UnityEngine;
using System.Collections;

namespace MyNamespace
{
    [ExecuteInEditMode]
    public class UniversalScale : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 10)]
        private float scaleFactor;
        public float ScaleFactor
        {
            get
            {
                return scaleFactor;
            }

            set
            {
                isDirty = true;
                scaleFactor = value;
            }
        }

        //[SerializeField]
        //[ReadOnly]
        private Vector3 scale = Vector3.zero;

        private bool isDirty;

        [SerializeField]
        private bool newScale;

        public void Log()
        {
            Debug.Log(scale * 3);
        }
        
        private void Update ()
        {
            if ( isDirty )
            {
                Scale();
            }
        }

        private void Scale()
        {
            Vector3 newScale = Vector3.one * scaleFactor;
            //TODO: add set up for not scaling children
            transform.localScale = newScale;
            isDirty = false;
            scale = transform.localScale;
        } 
    }
}