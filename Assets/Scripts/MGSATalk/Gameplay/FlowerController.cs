using MGSATalk.Data;
using UnityEngine;

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

        public void Init(FlowerTemplate template = null)
        {
            this.template = template;
            renderer.sprite = template.Agent;
            if (template != null)
            {
                template.Init(this);
            }
        }
    }
}
