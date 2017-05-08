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

        public override void GardenerTendTo()
        {
            throw new System.NotImplementedException();
        }

        protected override void UpdateFlower(FlowerController control)
        {
            Vector3 current = control.transform.localScale;
            Vector3 next = current;
            next.x = nextGoalSize;
            next.y = nextGoalSize;
            control.transform.localScale = Vector3.Lerp(current, next, Time.deltaTime * growthSpeed);
        }

        public override void Init(FlowerController controller)
        {
            Vector3 scale = controller.transform.localScale;
            scale.x *= 0.1f;
            scale.y *= 0.1f;
            controller.transform.localScale = scale;
        }
    }
}
