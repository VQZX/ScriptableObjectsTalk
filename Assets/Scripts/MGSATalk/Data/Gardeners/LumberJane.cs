using MGSATalk.Data.Flowers;
using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data.Gardeners
{
    /// <summary>
    /// LumberJane inherits from GardenerTemplate
    /// </summary>
    ///
    /// Attribute to make it easier to create this specific type of object
    /// filename: The default file name for the asset
    /// menuName: The order within the create menu
    /// order: positioning within the create menu
    [CreateAssetMenu(fileName = "LumberJane.asset", menuName = "MGSATalk/Gardener/LumberJane", order = 1)]
    public class LumberJane : GardenerTemplate
    {

        /// <summary>
        /// Override TendFlower from base class
        /// LumerJane wont tend to roses
        /// otherwise will tend
        /// </summary>
        /// <param name="gardener"></param>
        /// <param name="flower"></param>
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
