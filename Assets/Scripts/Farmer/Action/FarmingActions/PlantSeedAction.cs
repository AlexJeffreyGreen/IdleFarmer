using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Farmer.Action;
using Assets.Scripts.Plants;
using UnityEngine;

namespace Assets.Scripts.Farmer.Action.FarmingActions
{
    public class PlantSeedAction : ActionBase
    {
        protected override string Name => $"Planting {this.Seed.Name} at {this.GridPosition.x}, {this.GridPosition.y}, {this.GridPosition.z}";

        public PlantSeedAction(Vector3Int gridLocation, Seed seed) : base(gridLocation, seed)
        {
        }
    }
}
