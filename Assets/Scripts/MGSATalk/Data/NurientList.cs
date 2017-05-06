using UnityEngine;

namespace MGSATalk.Data
{
    public class NurientList : ScriptableObject
    {
        [SerializeField] protected Nutrient[] nutrients;

        public Nutrient this[int i]
        {
            get { return nutrients[i]; }
        }

    }
}
