using MGSATalk.Gameplay;
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

        public override void GardenerTendTo(FlowerController controller)
        {
            controller.NextGoalSize += incrementPercentage;
            controller.Growing = true;
        }

        public override void UpdateFlower(FlowerController control)
        {
            if (!control.Growing)
            {
                return;
            }
            Vector3 current = control.transform.localScale;
            Vector3 next = current;
            next.x = control.NextGoalSize;
            next.y = control.NextGoalSize;
            control.transform.localScale = Vector3.Lerp(current, next, Time.deltaTime * growthSpeed);
            if (Vector3.Distance(current, next) < 0.01)
            {
                control.Growing = false;
            }
        }

        public override void Init(FlowerController controller)
        {
            Vector3 scale = controller.transform.localScale;
            scale.x *= 0.1f;
            scale.y *= 0.1f;
            nextGoalSize = scale.x;
            controller.NextGoalSize = nextGoalSize;
            controller.transform.localScale = scale;
        }
    }
}
