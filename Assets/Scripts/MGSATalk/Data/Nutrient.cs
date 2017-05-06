using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data
{
    [CreateAssetMenu(fileName = "Nurient", menuName = "MGSATalk/Nutrient", order = 1)]
    public abstract class Nutrient : AgentTemplate
    {
        protected abstract void WorkOnFlower(FlowerController flower);
    }
}
