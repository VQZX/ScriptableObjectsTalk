using UnityEngine;

/// <summary>
/// Base class for Gardeners and Flowers 
/// </summary>
public class AgentTemplate : ScriptableObject, IGardenData
{
    /// <summary>
    /// common data
    /// </summary>

    public int ID;

    public string AgentName;

    public Sprite AgentImage;

}