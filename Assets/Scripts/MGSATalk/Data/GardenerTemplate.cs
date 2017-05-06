using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data
{
    public abstract class GardenerTemplate : AgentTemplate
    {
        public abstract void TendFlower(GardenerController gardener, FlowerController flower);
    }
}
