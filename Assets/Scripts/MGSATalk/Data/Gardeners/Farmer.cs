using System;
using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data.Gardeners
{
    [CreateAssetMenu(fileName = "Farmer.asset", menuName = "MGSATalk/Gardener/Farmer", order = 1)]
    public class Farmer : GardenerTemplate
    {
        public override void TendFlower(GardenerController gardener, FlowerController flower)
        {
            throw new NotImplementedException();
        }
    }
}
