using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Flusk.Utility
{
    public static class MonobehaviourUtility
    {
        public static T SafeGetComponent<T> ( this MonoBehaviour mono ) where T : Component
        {
            T component = mono.gameObject.GetComponent<T>();
            if ( component == null )
            {
                component = mono.gameObject.AddComponent<T>();
            }
            return component;
        }

        public static T SafeGetComponent<T>(this GameObject mono) where T : Component
        {
            T component = mono.gameObject.GetComponent<T>();
            if (component == null)
            {
                component = mono.gameObject.AddComponent<T>();
            }
            return component;
        }

        public static void SafeInvoke ( this UnityEvent unityEvent )
        {
            if ( unityEvent != null )
            {
                unityEvent.Invoke();
            }
        }

        public static bool IsDestroyed ( this GameObject gameObject )
        {
            bool isNull = gameObject == null;
            bool cleanedUp = !ReferenceEquals(gameObject, null);
            return isNull && cleanedUp;
        }

        public static Bounds GetBoundsSomehow ( this GameObject gameObject )
        {
            Renderer render = gameObject.GetComponent<Renderer>();
            Collider2D collider = gameObject.GetComponent<Collider2D>();
            Bounds output = new Bounds();
            //prioritise renderer
            if ( render != null )
            {
                output = render.bounds;
            }
            else if ( collider != null )
            {
                output = collider.bounds;
            }
            else
            {
                throw new System.Exception("Bounds not found on object");
            }
            return output;
        }
    } 
}
