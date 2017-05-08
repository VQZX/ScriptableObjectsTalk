using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.MGSATalk.Gameplay
{
    public class GardenPiece : MonoBehaviour
    {
        [SerializeField]
        protected SpriteRenderer spriteRender;
        [SerializeField]
        protected Collider2D collider2D;

        public Bounds GetBounds()
        {
            return spriteRender.bounds;
        }
    }
}
