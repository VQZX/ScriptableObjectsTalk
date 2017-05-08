using System;
using System.Diagnostics;
using UnityEngine;

namespace Flusk.Utilities.Prototypes
{
    public class Timer
    {
        private float legnth = 0;
        private Action callback = null;

        public Timer(float length, Action callback = null)
        {
            this.legnth = length;
            this.callback = callback;
        }

        public float ReturnLength()
        {
            return legnth;
        }

        public void Assign()
        {
            callback = Hello;
            callback();
        }

        private void Hello()
        {
            UnityEngine.Debug.Log("Hello");
        }
    }
}