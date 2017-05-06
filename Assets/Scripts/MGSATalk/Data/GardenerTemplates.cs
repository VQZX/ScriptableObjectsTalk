using UnityEngine;

namespace MGSATalk.Data
{
    public class GardenerTemplates : ScriptableObject
    {
        [SerializeField]
        protected GardenerTemplate[] gardenerTemplates;

        public GardenerTemplate this[int i]
        {
            get { return gardenerTemplates[i]; }
        }
    }
}
