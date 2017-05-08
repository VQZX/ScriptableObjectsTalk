using System;
using MGSATalk.Data;
using UnityEngine;

namespace MGSATalk.Gameplay
{
    public class GardenController : Controller
    {
        [SerializeField] public GardenTemplate template;

        public static Action GardenInitialized;

        protected virtual void Awake()
        {
            int width = template.Width;
            int height = template.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int index = x * height + y;
                    Debug.Log("Index "+index);
                }
            }
        }
    }
}
