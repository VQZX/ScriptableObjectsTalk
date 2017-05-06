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

        [SerializeField]
        [ReadOnly]
        private Vector3 scale;

        private bool isDirty;

        [SerializeField]
        private bool newScale; 
        
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