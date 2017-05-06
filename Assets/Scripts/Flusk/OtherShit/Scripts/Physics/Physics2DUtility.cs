using UnityEngine;
using System.Collections;
using Honours.Helpers;

namespace Flusk.Utility 
{
	public static class Physics2DUtility 
	{
		public static RaycastHit2D [] MultiRaycast ( Vector3 origin, int rayAmounts )
		{
			return MultiRaycast( origin, rayAmounts, Mathf.Infinity );
		}
	
		public static RaycastHit2D [] MultiRaycast ( Vector3 origin, int rayAmounts, float length )
		{
			LayerMask mask = LayerMask.NameToLayer("Everything");
			return MultiRaycast ( origin, rayAmounts, length, mask);
		}
	
		public static RaycastHit2D [] MultiRaycast ( Vector3 origin, int rayAmounts, float length, LayerMask mask )
		{
			return MultiRaycast ( origin, rayAmounts, length, mask, -Mathf.Infinity);
		}
	
		public static RaycastHit2D [] MultiRaycast ( Vector3 origin, int rayAmounts, float length, LayerMask mask, float minDepth )
		{
			return MultiRaycast ( origin, rayAmounts, length, mask, minDepth, Mathf.Infinity );
		}
	
		public static RaycastHit2D [] MultiRaycast ( Vector3 origin, int rayAmounts, float length, LayerMask mask, float minDepth, float maxDepth )
		{
			RaycastHit2D [] hits = new RaycastHit2D[ rayAmounts ];
			float deltaAngle = ( Mathf.PI * 2 ) / rayAmounts;
			for ( int i = 0; i < rayAmounts; i++ )
			{
				Vector2 direction = ( deltaAngle * i ).AngleToVector( AngleParadigm.Radians );
				hits[i] = Physics2D.Raycast( (Vector2) origin, direction, length, mask, minDepth, maxDepth );
			}
			return hits;
		}

        public static RaycastHit2D [] MultiRaycastWithOffset ( Vector3 origin, int rayAmounts, float length, LayerMask mask, float min = -Mathf.Infinity, float max = Mathf.Infinity, float offset = 0 )
        {
            RaycastHit2D[] hits = new RaycastHit2D[rayAmounts];
            float deltaAngle = (Mathf.PI * 2) / rayAmounts;
            for (int i = 0; i < rayAmounts; i++)
            {
                Vector2 direction = (deltaAngle * i).AngleToVector(AngleParadigm.Radians);
                Vector2 offsetOrigin = ( (Vector2) origin ) + offset * direction;
                hits[i] = Physics2D.Raycast( offsetOrigin, direction, length, mask, min, max);
            }
            return hits;
        }

		public static void DrawMultiRay (Vector3 origin, int rayAmounts )
		{
			float deltaAngle = ( Mathf.PI * 2 ) / rayAmounts;
			for ( int i = 0; i < rayAmounts; i++ )
			{
				Vector2 direction = ( deltaAngle * i ).AngleToVector( AngleParadigm.Radians );
				Gizmos.DrawRay( origin, direction );
			}
		}

		public static bool ColliderExists ( this RaycastHit2D raycast )
		{
			return raycast.collider != null;
		}

		public static bool MutlipleColliderExists( this RaycastHit2D [] raycasts )
		{
			bool colliderExists = true;
			for ( int i = 0; i < raycasts.Length; i++ )
			{
				colliderExists = colliderExists && ( raycasts[i].ColliderExists() );
			}
			return colliderExists;
		}

        public static bool MutlipleColliderExists(this RaycastHit2D[] raycasts, out string list)
        {
            bool colliderExists = true;
            list = "Begin: ";
            for (int i = 0; i < raycasts.Length; i++)
            {
                colliderExists = colliderExists && (raycasts[i].ColliderExists());
                if ( !colliderExists )
                {
                    break;
                }
                Collider2D col = raycasts[i].collider;
                list = string.Format("{0}\n {2} {1}", list, col.name, i);
            }
            return colliderExists;
        }

    }
}
