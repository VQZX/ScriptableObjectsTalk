using UnityEngine;

namespace MGSATalk.Data
{
    public abstract class AgentTemplate : ScriptableObject, IGardenData
    {
        [SerializeField]
        protected int id;
        public int ID
        {
            get { return id; }
        }

        [SerializeField]
        protected string agentName;

        public string Name
        {
            get { return agentName; }
        }

        [SerializeField]
        protected Sprite agentImage;

        public Sprite Agent
        {
            get { return agentImage; }
        }
    }
}
