using System;
using MGSATalk.Data;
using UnityEngine;

namespace MGSATalk.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GardenerController : MonoBehaviour
    {
        [SerializeField] protected GardenerTemplate template;
        [SerializeField] protected new SpriteRenderer renderer;

        public static event Action<string> GardenerInitialized;


        public void Initialize(GardenerTemplate template)
        {
            renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = template.Agent;
            this.template = template;
            if (GardenerInitialized != null)
            {
                GardenerInitialized(template.Name);
            }
            template.Init(this);
        }

    }
}
