using UnityEngine;
using System.Collections;

namespace Flusk.Animations
{
    public class AnimationCallbacks : MonoBehaviour
    {
        public delegate void AnimationCall (string stateName);
        public event AnimationCall OnAnimationBegin;
        public event AnimationCall OnAnimationEnd;

        public void CallBegin ( string stateName )
        {
            if ( OnAnimationBegin != null )
            {
                OnAnimationBegin(stateName);
            }
        }

        public void CallEnd ( string stateName )
        {
            if (OnAnimationEnd != null)
            {
                OnAnimationEnd(stateName);
            }
        }
    } 
}
