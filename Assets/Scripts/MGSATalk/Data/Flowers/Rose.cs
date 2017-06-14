using MGSATalk.Gameplay;
using UnityEngine;

/// <summary>
/// Rose class inherits from FlowerTemplate
/// </summary>
///
/// Attribute to make it easier to create this specific type of object
/// filename: The default file name for the asset
/// menuName: The order within the create menu
/// order: positioning within the create menu
[CreateAssetMenu(fileName = "Rose.asset", menuName = "MGSATalk/Flower/Rose", order = 1)]
public class Rose : FlowerTemplate
{

    /// <summary>
    /// Initialisation code called from the FlowerController (the Monobehaviour referenced by this SciptableObject)
    /// to set up any necessary data
    /// OVERRIDEN FROM BASE CLASS "FlowerTemplate"
    /// </summary>
    /// <param name="controller"></param>
    public override void GardenerTendTo(FlowerController controller)
    {
        controller.NextGoalSize += incrementPercentage;
        controller.Growing = true;
    }

    // <summary>
    /// Initialisation for when the gardener tends to the flower
    /// Only exists to be inherited, thus the function is abstract
    /// FOR BAMBOO!
    /// all we is update the size level of the object in the game
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
        next.x = control.NextGoalSize;
        next.y = control.NextGoalSize;
        control.transform.localScale = Vector3.Lerp(current, next, Time.deltaTime * growthSpeed);
        if (Vector3.Distance(current, next) < 0.01)
        {
            control.Growing = false;
        }
    }

    /// <summary>
    /// Initialisation code called from the FlowerController (the Monobehaviour referenced by this SciptableObject)
    /// to set up any necessary data
    /// OVERRIDEN FROM BASE CLASS "FlowerTemplate"
    /// </summary>
    /// <param name="controller"></param>
    public override void Init(FlowerController controller)
    {
        Vector3 scale = controller.transform.localScale;
        scale.x *= 0.1f;
        scale.y *= 0.1f;
        nextGoalSize = scale.x;
        controller.NextGoalSize = nextGoalSize;
        controller.transform.localScale = scale;
    }
}