using UnityEngine;

namespace MGSATalk.Data
{
    /// <summary>
    /// Base class for Gardeners and Flowers 
    /// </summary>
    public class AgentTemplate : ScriptableObject, IGardenData
    {
        /// <summary>
        /// common data
        /// </summary>

        public int id;
        public int ID
        {
            get { return id; }
        }

        public string agentName;

        public string Name
        {
            get { return agentName; }
        }

        public Sprite agentImage;

        public Sprite Agent
        {
            get { return agentImage; }
        }
    }
}
