using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data
{
    public abstract class GardenerTemplate : AgentTemplate
    {
        public float speed = 5;
        protected bool isTendingFlower = false;
        public abstract void TendFlower(GardenerController gardener, FlowerController flower);

        public Transform from;

        public virtual void Init(GardenerController controller)
        {
            Debug.Log("Initialized");
        }

        public virtual void UpdateGardener(GardenerController controller)
        {
            Vector3 goal = controller.CurrentFlower.transform.position;
            Vector3 current = controller.transform.position;
            current = Vector3.Lerp(current, goal, Time.deltaTime * speed);
            controller.transform.position = current;
            float distance = Vector3.Distance(goal, current);
            //Debug.Log("Distance: " + distance);
            bool close = distance < 0.1f;
            if (close)
            {
                TendFlower(controller, controller.CurrentFlower);
                controller.CurrentFlower = null;
            }
        }
    }
}
