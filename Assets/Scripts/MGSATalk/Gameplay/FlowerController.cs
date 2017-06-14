using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerController : MonoBehaviour
{
    [SerializeField] protected FlowerTemplate template;
    public FlowerTemplate Template
    {
        get { return template; }
        set { template = value; }
    }
    [SerializeField] protected new SpriteRenderer renderer;

    public bool Growing { get; set; }
    public float NextGoalSize { get; set; }

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

    protected virtual void Update()
    {
        template.UpdateFlower(this);
    }

    public void Tend()
    {
        Growing = true;
        template.GardenerTendTo(this);
    }
}