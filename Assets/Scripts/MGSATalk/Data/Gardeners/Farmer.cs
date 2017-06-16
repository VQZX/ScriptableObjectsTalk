using System;
using MGSATalk.Data.Flowers;
using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data.Gardeners
{
    /// <summary>
    /// Farmer inherits from GardenerTemplate
    /// </summary>
    ///
    /// Attribute to make it easier to create this specific type of object
    /// filename: The default file name for the asset
    /// menuName: The order within the create menu
    /// order: positioning within the create menu
    [CreateAssetMenu(fileName = "Farmer.asset", menuName = "MGSATalk/Gardener/Farmer", order = 1)]
    public class Farmer : GardenerTemplate
    {
        /// <summary>
        /// Override TendFlower from base class
        /// Farmer won't tend to Bamboo
        /// otherwise will tend to others
        /// </summary>
        /// <param name="gardener"></param>
        /// <param name="flower"></param>
        public override void TendFlower(GardenerController gardener, FlowerController flower)
        {
            isTendingFlower = true;
            if (flower.Template is Bamboo)
            {
                gardener.Refuse();
                return;
            }
            gardener.AcceptTend();
            flower.Tend();
        }
    }
}
