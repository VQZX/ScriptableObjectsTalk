using UnityEngine;

namespace MGSATalk.Data
{
    public abstract class FlowerTemplate : ScriptableObject
    {
        public abstract void AcceptIngredients(Nutrient[] nutrients);
    }
}
