using System.Data.Common;
using UnityEngine;

namespace Farmer.Action
{
    public abstract class ActionBase
    {
        protected Vector3Int GridPosition { get; }
        protected abstract string Name { get; }
        protected abstract int ActionStaminaOffset { get; }
        protected  abstract  float[] Percentages { get; }
        protected  abstract Sprite[] Sprites { get; }

        public virtual int GetActionStaminaOffSet()
        {
            return 0;
        }
        //public string ActionName;
        //public int ActionStaminaOffset;
        //public float[] Percentages;
        //public Sprite[] Sprites;
        //private Vector2Int GridPosition;
        
        public ActionBase(Vector3Int gridLocation)
        {
            this.GridPosition = gridLocation;
        }

        public ActionBase()
        {
            
        }
    }
}