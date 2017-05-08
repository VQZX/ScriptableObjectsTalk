using MGSATalk.Gameplay;
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

        public override void GardenerTendTo()
        {
            throw new System.NotImplementedException();
        }

        protected override void UpdateFlower(FlowerController control)
        {
            Vector3 current = control.transform.localScale;
            Vector3 next = current;
            next.y = nextGoalSize;
            control.transform.localScale = Vector3.Lerp(current, next, Time.deltaTime * growthSpeed);
        }
    }
}
