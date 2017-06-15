using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GardenerController : MonoBehaviour
{
    public GardenerTemplate Template;
    
    public SpriteRenderer Renderer;
    
    public Animator Animator;

    /// <summary>
    /// a callback for when the gardener is initialised to prevent any access errors
    /// on the gardener
    /// </summary>
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

    /// <summary>
    /// Only tend to a flower if one is currently available
    /// </summary>
    protected virtual void Update()
    {
        if (CurrentFlower == null)
        {
            return;
        }
        Template.UpdateGardener(this);
    }

    /// <summary>
    /// Initialize the Gardener from the GardenerTemplate scriptable object
    /// </summary>
    /// <param name="template"></param>
    public void Initialize(GardenerTemplate template)
    {
        Renderer = GetComponent<SpriteRenderer>();
        Renderer.sprite = template.AgentImage;
        Template = template;
        if (GardenerInitialized != null)
        {
            GardenerInitialized(template.AgentName);
        }
        template.Init(this);
    }

    /// <summary>
    /// Feedback when the gardener cannot tend a plant
    /// </summary>
    public void Refuse()
    {
        Animator.SetBool("Refuse", true);
    }

    /// <summary>
    /// Feedback when the gardener happilly tends the plant
    /// </summary>
    public void AcceptTend()
    {
        Animator.SetBool("Work", true);
    }

    //Callack from the FlowerController when a new flower has been selecte
    private void OnFlowerSelected(FlowerController controller)
    {
        Animator.SetBool("Refuse", false);
        Animator.SetBool("Work", false);
        Debug.Log("FlowerSelected "+controller.Template.GetType());
        CurrentFlower = controller;
    }

}