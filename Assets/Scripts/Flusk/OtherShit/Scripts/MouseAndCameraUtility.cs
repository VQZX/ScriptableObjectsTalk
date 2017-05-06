using UnityEngine;
using System.Collections;
using UnityCamera = UnityEngine.Camera;

namespace Flusk.Utility
{
    public static class MouseAndCameraUtility
    {
        public static Vector3 DirectionToMouse ( this UnityEngine.Transform trans )
        {
            Vector3 output = Vector3.zero;
            Vector3 worldMouse = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            worldMouse.z = 0;
            output = worldMouse - trans.position;
            return output;
        }

        public static Vector3 ViewToWorld (Vector2 pos )
        {
            UnityCamera camera = UnityCamera.main;
            if ( camera != null )
            {
                Vector3 world = UnityEngine.Camera.main.ViewportToWorldPoint(pos);
                world.z = 0;
                return world;
            }
            return Vector3.zero;
            
        }

        public static Rect ViewRectToWorld ()
        {
            Vector3 bottomLeft = UnityCamera.main.ViewportToWorldPoint(Vector3.zero);
            Vector3 topRight = UnityCamera.main.ViewportToWorldPoint(Vector3.one);
            Vector3 center = UnityCamera.main.ViewportToWorldPoint(Vector3.one * 0.5f);

            bottomLeft.z = 0;
            topRight.z = 0;
            center.z = 0;

            float width = topRight.x - bottomLeft.x;
            float height = topRight.y - bottomLeft.y;

            Vector2 size = new Vector2(width, height);

            Rect output = new Rect (  (Vector2) bottomLeft, size);

            return output;

        }
    }

}
