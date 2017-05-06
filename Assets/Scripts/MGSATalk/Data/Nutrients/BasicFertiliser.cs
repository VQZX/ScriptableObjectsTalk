using System;
using MGSATalk.Gameplay;
using UnityEngine;

namespace MGSATalk.Data.Nutrients
{
    [CreateAssetMenu(fileName = "Fertiliser.asset", menuName = "MGSATalk/Nutrient/Fertiliser", order = 1)]
    public class BasicFertiliser : Nutrient
    {
        protected override void WorkOnFlower(FlowerController flower)
        {
            throw new NotImplementedException();
        }
    }
}
