using UnityEngine;

namespace MGSATalk.Data
{
    [CreateAssetMenu(fileName = "Flower.asset", menuName = "MGSATalk/Flower", order = 1)]
    public abstract class FlowerTemplate : AgentTemplate
    {
        public abstract void AcceptIngredients(Nutrient[] nutrients);
    }
}
