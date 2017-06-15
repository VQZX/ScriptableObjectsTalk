using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// An object in the scene that represents one piece of a garden
/// Is interactable
/// </summary>
public class GardenPiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public SpriteRenderer SpriteRenderer;

    public Collider2D Collider2D;

    public Bounds GetBounds()
    {
        return SpriteRenderer.bounds;
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