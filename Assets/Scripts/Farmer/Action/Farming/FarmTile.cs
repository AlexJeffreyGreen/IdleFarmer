using Assets.Scripts.Plants;
using UnityEngine;

namespace Farmer.Action.Farming
{
    public class FarmTile : ActionBase
    {
        
        public FarmTile(Vector3Int gridLocation) : base(gridLocation)
        {
            
        }

        public FarmTile()
        {
            //throw new System.NotImplementedException();
        }

        public override int GetActionStaminaOffSet()
        {
            return -100;
        }

        protected override string Name { get; }
        protected override int ActionStaminaOffset { get; }
        protected override float[] Percentages { get; }
        protected override Seed Seed { get; }
    }
}