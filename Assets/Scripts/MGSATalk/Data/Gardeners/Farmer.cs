using System;
using MGSATalk.Data.Flowers;
using MGSATalk.Gameplay;
using UnityEngine;
using Assets.Scripts.MGSATalk.Data.Flowers;

namespace MGSATalk.Data.Gardeners
{
    [CreateAssetMenu(fileName = "Farmer.asset", menuName = "MGSATalk/Gardener/Farmer", order = 1)]
    public class Farmer : GardenerTemplate
    {
        public Transform transform;

        public newClass hello;

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
