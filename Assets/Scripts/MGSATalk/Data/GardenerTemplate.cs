using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data
{
    public abstract class GardenerTemplate : AgentTemplate
    {
        public abstract void TendFlower(GardenerController gardener, FlowerController flower);

        public virtual void Init(GardenerController controller)
        {
            
        }
    }
}
