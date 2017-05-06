using UnityEngine;
using System.Collections;
using System;

namespace Flusk.Utility
{
    public static class TimeSpanUtility
    {
        public static string ToMinutesAndSeconds (this TimeSpan span )
        {
            int _minutes = span.Minutes;
            int _seconds = span.Seconds;

            string minutes = _minutes.ToString("D2");
            string seconds = _seconds.ToString("D2");

            string format = string.Format("{0}:{1}", minutes, seconds);
            return format;
        }
    } 

    
}
