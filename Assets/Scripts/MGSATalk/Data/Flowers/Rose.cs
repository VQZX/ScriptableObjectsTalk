using UnityEngine;

namespace MGSATalk.Data.Flowers
{
    [CreateAssetMenu(fileName = "Rose.asset", menuName = "MGSATalk/Flower/Rose", order = 1)]
    public class Rose : FlowerTemplate
    {
        public override void AcceptIngredients(Nutrient[] nutrients)
        {
            throw new System.NotImplementedException();
        }
    }
}
