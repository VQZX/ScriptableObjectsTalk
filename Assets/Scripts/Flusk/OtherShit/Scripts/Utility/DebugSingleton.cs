using UnityEngine;
using System.Collections;

namespace Flusk.Helpers
{
    public enum DebugLevel
    {
        Trivial = 0,
        Minor = 1,
        Major = 2,
        Critical = 3,
        Severe = 4
    }

    public enum DebugType
    {
        Info = 0,
        Warning = 1,
        Error = 2
    }

    public struct DebugObject
    {
        public object sender;
        public string log;
        public DebugType type;
        public DebugLevel level;
        public Color color;
    }

    public class DebugSingleton : PersistentSingleton<DebugSingleton>
    {
        private Color defaultColor = Color.black;

        public Color DefaultColor
        {
            get
            {
                return defaultColor;
            }
        }

        public static DebugSingleton Current ()
        {
            return instance as DebugSingleton;
        }

        public void Log ( string log )
        {
            Color col = Color.black;
            Log(log, col);
        }

        public void Log (string log, Color color)
        {
            //add the color option later
            if ( Application.isEditor )
            {
                Debug.Log(log);
            } 
        }

        public void Log(string log, bool mustWrite)
        {
            if ( mustWrite )
            {
                Log(log, DefaultColor);
            }
        }

        public void Log( object sender, string log )
        {

        }

    }
}