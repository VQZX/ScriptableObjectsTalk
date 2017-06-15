using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerController : MonoBehaviour
{
    public FlowerTemplate Template;
    public SpriteRenderer Renderer;

    public bool Growing { get; set; }
    public float NextGoalSize { get; set; }

    public static event Action<FlowerController> Selected;

    public void Init(FlowerTemplate template = null)
    {
        if (template == null)
        {
            return;
        }
        Template = template;
        Renderer.sprite = template.AgentImage;
        template.Init(this);
    }

    public void PointerClick()
    {
        Debug.Log("OnPointerClick");
        if (Selected != null)
        {
            Selected(this);
        }
    }

    protected virtual void Update()
    {
        Template.UpdateFlower(this);
    }

    public void Tend()
    {
        Growing = true;
        Template.GardenerTendTo(this);
    }
}