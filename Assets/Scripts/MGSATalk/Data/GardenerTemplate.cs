using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data
{
    public abstract class GardenerTemplate : ScriptableObject
    {
        [SerializeField] protected int id;
        public int ID
        {
            get { return id; }
        }

        [SerializeField] protected string gardenerName;

        public string Name
        {
            get { return gardenerName; }
        }

        [SerializeField] protected Sprite gardenerImage;

        public Sprite Gardener
        {
            get { return gardenerImage; }
        }

        public abstract void TendFlower(GardenerController gardener, FlowerController flower);
    }
}
