using UnityEngine;
using UnityEngine.EventSystems;

namespace MGSATalk.Gameplay
{
    public class GardenPiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField]
        protected SpriteRenderer spriteRender;
        [SerializeField]
        protected new Collider2D collider2D;

        public Bounds GetBounds()
        {
            return spriteRender.bounds;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var flower = GetComponentInChildren<FlowerController>();
            if (flower != null)
            {
                flower.PointerClick(); ;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}
