using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Farmer.Action.FarmingActions
{
    public class WaterAction : ActionBase
    {
        protected override string Name => $"Watering tile at location {this.GridPosition.x}, {this.GridPosition.y}, {this.GridPosition.z}";
    }
}
