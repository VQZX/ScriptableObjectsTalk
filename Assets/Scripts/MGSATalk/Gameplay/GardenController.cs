using System;
using Assets.Scripts.MGSATalk.Gameplay;
using MGSATalk.Data;
using UnityEngine;

namespace MGSATalk.Gameplay
{
    public class GardenController : Controller
    {
        [SerializeField] public GardenTemplate template;
        [SerializeField] protected GameObject gardenPiece;

        public static Action GardenInitialized;

        protected virtual void Awake()
        {
            int width = template.Width;
            int height = template.Height;
            var gp = gardenPiece.GetComponent<GardenPiece>();
            var bounds = gp.GetBounds();
            var w = bounds.size.x;
            var h = bounds.size.y;
            var initPos = transform.position;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int index = x * height + y;
                    if (!template[index])
                    {
                        continue;
                    }
                    var pos = initPos + new Vector3( w * x, h * y);
                    Create(pos, x, y);
                }
            }
        }

        private GameObject Create(Vector3 pos, int x, int y)
        {
            var g =  (GameObject) Instantiate(gardenPiece, pos, Quaternion.identity);
            g.transform.SetParent(transform);
            g.name += string.Format("({0}, {1})", x, y);
            return g;
        }
    }
}
