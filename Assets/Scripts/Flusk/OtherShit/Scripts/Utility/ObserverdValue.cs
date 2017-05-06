using UnityEngine;
using System;

namespace Flusk.Utility
{
    [Serializable]
    public class ObserverdValue<T>
    {
        [SerializeField]
        private T observedValue;
        public T Value
        {
            get
            {
                return observedValue;
            }

            set
            {
                if (OnValueChanged != null)
                {
                    OnValueChanged(observedValue, value);
                }
                if (OnValueChangedGlobal != null)
                {
                    OnValueChangedGlobal(observedValue, value);
                }
                observedValue = value;
            }
        }
        private Action<T, T> OnValueChanged;
        private static Action<T, T> OnValueChangedGlobal;

        public ObserverdValue ( T defaultValue )
        {
            observedValue = defaultValue;
        }

        public void SetCallback ( Action<T,T> action )
        {
            OnValueChanged += action;
        }

        public void SetGlobalCallback( Action<T,T> action )
        {
            OnValueChangedGlobal += action;
        }
        
    }

}