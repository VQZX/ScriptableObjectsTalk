using System;

namespace Flusk.Utilities.Prototypes
{
    public class Timer
    {
        private float legnth;
        private Action callback;

        public Timer(float length, Action callback = null)
        {
            this.legnth = length;
            this.callback = callback;
        }
    }
}