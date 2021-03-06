﻿using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data
{
    /// <summary>
    /// Base class for flower data
    /// </summary>
    ///
    /// Attribute to make it easier to create this specific type of object
    /// filename: The default file name for the asset
    /// menuName: The order within the create menu
    /// order: positioning within the create menu
    [CreateAssetMenu(fileName = "Flower.asset", menuName = "MGSATalk/Flower", order = 1)]
    public abstract class FlowerTemplate : AgentTemplate
    {
        /// <summary>
        /// a string type for ease of type checking
        /// </summary>
        [SerializeField] public string type;

        public string Type
        {
            get { return type; }
        }

        /// <summary>
        /// The speed at which the flower grows every frame
        /// </summary>
        [SerializeField] public float growthSpeed = 1;
        /// <summary>
        /// The next level the flower will grow to when the gardener tends to it
        /// </summary>
        [SerializeField] public float incrementPercentage = 0.3f;

        public float nextGoalSize;

        /// <summary>
        /// Initialisation for when the gardener tends to the flower
        /// Only exists to be inherited, thus the function is abstract
        /// </summary>
        /// <param name="controller"></param>
        public abstract void GardenerTendTo(FlowerController controller);

        /// <summary>
        /// The continuous response of the flower once it has been tended to
        /// </summary>
        /// <param name="controller"></param>
        public abstract void UpdateFlower(FlowerController controller);

        /// <summary>
        /// Initialisation code called from the FlowerController (the Monobehaviour referenced by this SciptableObject)
        /// to set up any necessary data
        /// </summary>
        /// <param name="controller"></param>
        public virtual void Init(FlowerController controller)
        {
            Vector3 scale = controller.transform.localScale;
            scale.y *= 0.1f;
            controller.transform.localScale = scale;
        }
    }
}
