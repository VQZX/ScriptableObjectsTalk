using UnityEngine;

public class GardenAgentList<TData> : ScriptableObject where TData : IGardenData
{
    [SerializeField] protected TData[] agents;

    public TData this[int i]
    {
        get { return agents[i]; }
    }
}