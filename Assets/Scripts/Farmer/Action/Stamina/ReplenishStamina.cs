using Assets.Scripts.Plants;
using UnityEngine;

namespace Farmer.Action.Stamina
{
    public class ReplenishStamina : ActionBase
    {
        public ReplenishStamina() : base()
        {
            
        }

        protected override Seed Seed { get; }

        public override int GetActionStaminaOffSet()
        {
            return 100;
        }

        protected override string Name { get; }
        protected override int ActionStaminaOffset { get; }
        protected override float[] Percentages { get; }
        //protected override Sprite[] Sprites { get; }
    }
}