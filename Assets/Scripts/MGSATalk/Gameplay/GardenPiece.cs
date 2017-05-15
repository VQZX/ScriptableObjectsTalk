using UnityEngine;
using UnityEngine.EventSystems;

namespace MGSATalk.Gameplay
{

    /// <summary>
    /// An object in the scene that represents one piece of a garden
    /// Is interactable
    /// </summary>
    public class GardenPiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField]
        public SpriteRenderer spriteRender;
        [SerializeField]
        public new Collider2D collider2D;

        public Bounds GetBounds()
        {
            return spriteRender.bounds;
        }

        /// <summary>
        /// When the players clicks on a piece, we get the child flower controller
        /// and flag that a click on this piece has happened
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            var flower = GetComponentInChildren<FlowerController>();
            if (flower == null)
            {
                return;
            }
            flower.PointerClick(); ;
        }


        /// <summary>
        /// Implement these functions to make sure the PointerClick function fires off
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData) {}

        public void OnPointerUp(PointerEventData eventData) {}
    }
}
