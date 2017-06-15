using UnityEngine;

/// <summary>
/// Bamboo class inherits from FlowerTemplate
/// </summary>
///
/// Attribute to make it easier to create this specific Type of object
/// filename: The default file name for the asset
/// menuName: The order within the create menu
/// order: positioning within the create menu
[CreateAssetMenu(fileName = "Bamboo.asset", menuName = "MGSATalk/Flower/Bamboo", order = 1)]
public class Bamboo : FlowerTemplate
{
    /// <summary>
    /// Initialisation code called from the FlowerController (the Monobehaviour referenced by this SciptableObject)
    /// to set up any necessary data
    /// OVERRIDEN FROM BASE CLASS "FlowerTemplate"
    /// </summary>
    /// <param name="controller"></param>
    public override void Init(FlowerController controller)
    {
        base.Init(controller);
        controller.NextGoalSize = NextGoalSize;
    }

    /// <summary>
    /// Initialisation for when the gardener tends to the flower
    /// Only exists to be inherited, thus the function is abstract
    /// FOR BAMBOO!
    /// all we is update the size level of the object in the game
    /// </summary>
    /// <param name="controller"></param>
    public override void GardenerTendTo(FlowerController controller)
    {
        controller.NextGoalSize += IncrementPercentage;
    }

    /// <summary>
    /// The continuous response of the flower once it has been tended to
    /// </summary>
    /// <param name="controller"></param>
    public override void UpdateFlower(FlowerController control)
    {
        if (!control.Growing)
        {
            return;
        }
        Vector3 current = control.transform.localScale;
        Vector3 next = current;
        next.y = control.NextGoalSize;
        control.transform.localScale = Vector3.Lerp(current, next, Time.deltaTime * GrowthSpeed);
        if (Vector3.Distance(current, next) < 0.01)
        {
            control.Growing = false;
        }
    }
}