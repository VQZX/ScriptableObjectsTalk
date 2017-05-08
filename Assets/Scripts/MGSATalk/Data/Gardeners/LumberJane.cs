using MGSATalk.Data.Flowers;
using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data.Gardeners
{
    [CreateAssetMenu(fileName = "LumberJane.asset", menuName = "MGSATalk/Gardener/LumberJane", order = 1)]
    public class LumberJane : GardenerTemplate
    {

        public override void TendFlower(GardenerController gardener, FlowerController flower)
        {
            isTendingFlower = true;
            if (flower.Template is Rose)
            {
               gardener.Refuse();
                return;
            }
            gardener.AcceptTend();
            flower.Tend();
        }
    }
}
