using System.Globalization;
using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data
{
    [CreateAssetMenu(fileName = "Nurient", menuName = "MGSATalk/Nutrient", order = 1)]
    public abstract class Nutrient : ScriptableObject
    {
        [SerializeField] protected int id;
        public int ID
        {
            get { return id; }
        }

        [SerializeField]
        protected string nutrientName;
        public string NutrientName
        {
            get { return nutrientName; }
        }

        [SerializeField] protected Sprite nutrientImage;
        public Sprite NutrientImage
        {
            get { return nutrientImage; }
        }

        protected abstract void WorkOnFlower(FlowerController flower);
    }
}
