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

        public override void Init(FlowerController controller)
        {
            base.Init(controller);
            controller.NextGoalSize = nextGoalSize;
        }

        public override void GardenerTendTo(FlowerController controller)
        {
            controller.NextGoalSize += incrementPercentage;
        }

        public override void UpdateFlower(FlowerController control)
        {
            if (!control.Growing)
            {
                return;
            }
            Vector3 current = control.transform.localScale;
            Vector3 next = current;
            next.y = control.NextGoalSize;
            control.transform.localScale = Vector3.Lerp(current, next, Time.deltaTime * growthSpeed);
            if (Vector3.Distance(current, next) < 0.01)
            {
                control.Growing = false;
            }
        }
    }
}
