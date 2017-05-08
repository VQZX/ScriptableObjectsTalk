using UnityEngine;
using System.Collections;
using Flusk.Utility;
using Flusk.Helpers;

namespace Flusk.GraphicHelper
{
    [ExecuteInEditMode]
    public class ParentColorController : MonoBehaviour
    {
        private SpriteRenderer[] renderers;

        private bool isDirty = true;

        [SerializeField]
        private Color color;

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
                isDirty = true;
            }
        }

        private void Awake ()
        {
            renderers = GetComponentsInChildren<SpriteRenderer>();
        }

        private void Update ()
        {
            if ( Application.isPlaying )
            {
                ChangeColor();
            }
        }

        private void OnValidate ()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return;
            } 
#endif
        }

        private void ChangeColor ()
        {
            if (isDirty)
            {
                if (renderers != null)
                {
                    int length = renderers.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (!renderers[i].gameObject.IsDestroyed())
                        {
                            renderers[i].color = color;
                        }
                        else
                        {
                            //TODO: remove the object
                        }
                    }
                    isDirty = false;
                }
            }
        }
    } 
}
