using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data.Gardeners
{
    [CreateAssetMenu(fileName = "LumberJane.asset", menuName = "MGSATalk/Gardener/LumberJane", order = 1)]
    public class LumberJane : GardenerTemplate
    {
        public override void TendFlower(GardenerController gardener, FlowerController flower)
        {
            throw new System.NotImplementedException();
        }
    }
}
