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

        protected virtual void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = template.Agent;
            if (GardenerInitialized != null)
            {
                GardenerInitialized(template.Name);
            }
        }

    }
}
