using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Flusk.UIHelper
{
    [ExecuteInEditMode]
    public class ColourCanvas : MonoBehaviour
    {
        private Graphic[] childrenGraphics;

        private bool isDirty = false;

        [SerializeField]
        private Color panelColor;

        public Color PanelColor
        {
            get
            {
                return panelColor;
            }

            set
            {
                panelColor = value;
                isDirty = true;
            }
        }

        private void Awake ()
        {
            childrenGraphics = GetComponentsInChildren<Graphic>();
        }

        private void OnValidate ()
        {
            if ( isDirty )
            {
                int length = childrenGraphics.Length;
                for (int i = 0; i < length; i++)
                {
                    childrenGraphics[i].color = panelColor;
                }
                isDirty = false;
            }
        }
    } 
}
