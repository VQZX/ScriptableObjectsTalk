using UnityEngine;
using System.Collections;
using System;
using Flusk.Helpers.ScreenUtility;

namespace Flusk.Utilities
{
    public class CameraView : MonoBehaviour
    {
        private static Rect worldSpaceRect;
        public static Rect WorldSpaceRect
        {
            get
            {
                return worldSpaceRect;
            }
        }

        private Vector3 previousPosition = Vector3.zero;


        private void Update ()
        {
            Vector3 currentPosition = transform.position;
            if ( previousPosition != currentPosition )
            {
                UpdateRect();
            }
        }

        private void UpdateRect ()
        {
            worldSpaceRect = ScreenUtility.ScreenRectDimensions(UnityEngine.Camera.main.transform.position);
        }
    } 
}
