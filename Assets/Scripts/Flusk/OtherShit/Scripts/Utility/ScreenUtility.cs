using UnityEngine;
using System.Collections;
using UnityCamera = UnityEngine.Camera;

namespace Flusk.Helpers.ScreenUtility
{
    public static class ScreenUtility
    {

        public static Vector3 ScreenToWorld ( this Vector2 vector2 )
        {
            Vector3 output = UnityCamera.main.ScreenToWorldPoint(vector2);
            output.z = 0;
            return output;
        }

        public static Rect ScreenToWorld ( this Rect rect )
        {
            Rect output = new Rect();
            Vector2 position = new Vector2(rect.x, rect.y);
            Vector2 dimensions = new Vector2(rect.width, rect.height);

            position = UnityCamera.main.ScreenToWorldPoint(position);
            dimensions = UnityCamera.main.ScreenToWorldPoint(dimensions);

            output.x = position.x;
            output.y = position.y;
            output.width = dimensions.x;
            output.height = dimensions.y;

            return output;
        }

        public static Rect ScreenRectDimensions (Vector3 center)
        {
            Rect output = new Rect();

            Vector2 bottomLeft = Vector2.zero;
            Vector2 topRight = Vector2.one;

            bottomLeft = UnityCamera.main.ViewportToWorldPoint(bottomLeft);
            topRight = UnityCamera.main.ViewportToWorldPoint(topRight);

            output.width = topRight.x - bottomLeft.x;
            output.height = topRight.y - bottomLeft.y;
            output.center = center;

            return output;
        }

        public static bool WithinView(Vector3 position)
        {
            Vector3 cameraCenter = UnityCamera.main.transform.position;
            Rect worldView = ScreenRectDimensions(cameraCenter);
            bool withinBounds = worldView.Contains(position);
            return withinBounds;
        }
    } 
}
