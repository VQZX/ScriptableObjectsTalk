using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data
{
    [CreateAssetMenu(fileName = "Flower.asset", menuName = "MGSATalk/Flower", order = 1)]
    public abstract class FlowerTemplate : AgentTemplate
    {
        [SerializeField] protected string type;

        public string Type
        {
            get { return type; }
        }

        [SerializeField] protected float growthSpeed = 1;
        [SerializeField] protected float incrementPercentage = 0.3f;

        protected float nextGoalSize;

        public abstract void AcceptIngredients(Nutrient[] nutrients);

        public abstract void GardenerTendTo(FlowerController controller);

        public abstract void UpdateFlower(FlowerController controller);

        public virtual void Init(FlowerController controller)
        {
            Vector3 scale = controller.transform.localScale;
            scale.y *= 0.1f;
            controller.transform.localScale = scale;
        }
    }
}
