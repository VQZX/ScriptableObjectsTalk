using System;
using MGSATalk.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MGSATalk.Gameplay
{
    public class FlowerController : MonoBehaviour
    {
        [SerializeField] protected FlowerTemplate template;
        public FlowerTemplate Template
        {
            get { return template; }
            set { template = value; }
        }
        [SerializeField] protected new SpriteRenderer renderer;

        public static event Action<FlowerController> Selected;

        public void Init(FlowerTemplate template = null)
        {
            this.template = template;
            renderer.sprite = template.Agent;
            if (template != null)
            {
                template.Init(this);
            }
        }

        public void PointerClick()
        {
            Debug.Log("OnPointerClick");
            if (Selected != null)
            {
                Selected(this);
            }
        }


    }
}
