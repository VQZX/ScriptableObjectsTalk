using UnityEngine;

//TODO: consider removing generics
public class GardenAgentList<TData> : ScriptableObject where TData : IGardenData
{
    [SerializeField] 
    public TData[] Agents;

}