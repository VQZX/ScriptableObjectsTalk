using System;
using Assets.Scripts.MGSATalk.Gameplay;
using MGSATalk.Data;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace MGSATalk.Gameplay
{
    public class GardenController : Controller
    {
        [SerializeField] public GardenTemplate template;
        [SerializeField] protected GameObject gardenPiece;
        [SerializeField] protected FlowerTemplate[] flowers;
        [SerializeField] protected GameObject flowerPiece;
        [SerializeField] protected GardenerTemplate gardenerTemplate;
        [SerializeField] protected GameObject gardener;

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
                    int index = GardenTemplate.GetCorrectIndex(template, x, y);
                    if (!template[index])
                    {
                        continue;
                    }
                    var pos = initPos + new Vector3( w * x, -h * y);
                    var gpiece = Create(pos, x, y);
                    var flower  = CreateFlower(pos, gpiece);
                    var flowerControl = flower.GetComponent<FlowerController>();
                    FlowerTemplate flowerTemp = flowers[UnityRandom.Range(0, flowers.Length)];
                    flowerControl.Init(flowerTemp);
                }
            }
            CreateGardener(transform.position);
        }

        private GameObject Create(Vector3 pos, int x, int y)
        {
            var g =  (GameObject) Instantiate(gardenPiece, pos, Quaternion.identity);
            g.transform.SetParent(transform);
            g.name += string.Format("({0}, {1})", x, y);
            return g;
        }

        private GameObject CreateFlower(Vector3 pos, GameObject parent)
        {
            var flower = (GameObject) Instantiate(flowerPiece.gameObject);
            flower.transform.SetParent(parent.transform);
            flower.transform.localPosition = Vector3.zero;
            return flower;
        }

        private GameObject CreateGardener(Vector3 pos)
        {
            var gardener = Instantiate(this.gardener);
            gardener.transform.position = pos;
            var controller = gardener.GetComponent<GardenerController>();
            controller.Initialize(gardenerTemplate);
            return gardener;
        }
    }
}
