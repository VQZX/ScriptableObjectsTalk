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
        [SerializeField] protected new Animator animator;

        public static event Action<string> GardenerInitialized;

        public FlowerController CurrentFlower { get; set; }

        protected virtual void OnEnable()
        {
            FlowerController.Selected += OnFlowerSelected;
        }

        protected virtual void OnDisable()
        {
            FlowerController.Selected -= OnFlowerSelected;
        }

        protected virtual void Update()
        {
            if (CurrentFlower == null)
            {
                return;
            }
            template.UpdateGardener(this);
        }

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

        public void Refuse()
        {
            animator.SetBool("Refuse", true);
        }

        public void AcceptTend()
        {
            animator.SetBool("Work", true);
        }

        private void OnFlowerSelected(FlowerController controller)
        {
            animator.SetBool("Refuse", false);
            animator.SetBool("Work", false);
            Debug.Log("FlowerSelected "+controller.Template.GetType());
            CurrentFlower = controller;
        }

    }
}
