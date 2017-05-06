using UnityEngine;

namespace MGSATalk.Data.Flowers
{
    [CreateAssetMenu(fileName = "Bamboo.asset", menuName = "MGSATalk/Flower/Bamboo", order = 1)]
    public class Bamboo : FlowerTemplate
    {
        public override void AcceptIngredients(Nutrient[] nutrients)
        {
            throw new System.NotImplementedException();
        }
    }
}
