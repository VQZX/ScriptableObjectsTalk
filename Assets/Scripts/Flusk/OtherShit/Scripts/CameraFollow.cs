using UnityEngine;
using System.Collections;

namespace Flusk.Camera
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField]
		private UnityEngine.Transform target;

		[SerializeField]
		private float timeDelay = 15;

		private Vector3 speedTest;

		private void LateUpdate ()
		{
			Vector3 targetPosition = target.position;
			targetPosition.z = transform.position.z;
			transform.position = Vector3.SmoothDamp( transform.position, targetPosition, ref speedTest, Time.deltaTime * timeDelay );
			//maybe put a rect for a delay motion
		}
	}
}
