using UnityEngine;
using System.Collections;

namespace Flusk.Patterns
{
	public class TimerBehaviour : MonoBehaviour
	{
		protected float maxTimer = 5;
		protected float currentTimer = 0;
		protected TimeTypes timeType;
		protected bool allowTime = false;

		protected delegate void TimeFunction ();
		protected TimeFunction StartTime;
		protected TimeFunction ElapseTime;
		protected TimeFunction EndTime;

		protected virtual void Awake ()
		{
			currentTimer = maxTimer;
		}

		private float SelectDeltaTime ()
		{
			switch ( timeType )
			{
				case TimeTypes.Update:
				{
					return Time.deltaTime;
				}

				case TimeTypes.Unscale:
				{
					return Time.unscaledDeltaTime;
				}

				case TimeTypes.Fixed:
				{
					return Time.fixedDeltaTime;
				}
				default:
				{
					return 0;
				}
			}
		}

		protected virtual void LateUpdate ()
		{
			if ( allowTime && timeType != TimeTypes.Fixed )
			{
				if ( currentTimer == maxTimer )
				{
					if ( StartTime != null )
					{
						StartTime();
					}
				}
				currentTimer -= SelectDeltaTime();
				if ( ElapseTime != null )
				{
					ElapseTime();
				}
				if ( currentTimer <= 0 )
				{
					if ( EndTime != null )
					{
						EndTime();
					}
				}
			}
		}

		protected virtual void FixedUpdate ()
		{
			if ( allowTime && timeType == TimeTypes.Fixed )
			{
				if ( currentTimer == maxTimer )
				{
					if ( StartTime != null )
					{
						StartTime();
					}
				}
				currentTimer -= SelectDeltaTime();
				if ( ElapseTime != null )
				{
					ElapseTime();
				}
				if ( currentTimer <= 0 )
				{
					if ( EndTime != null )
					{
						EndTime();
					}
				}
			}
		}

	}
}
