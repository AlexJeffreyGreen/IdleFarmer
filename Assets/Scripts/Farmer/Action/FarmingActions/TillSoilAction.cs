using Farmer.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Farmer.Action.FarmingActions
{
    public class TillSoilAction : ActionBase
    {
        protected override string Name => "Till Soil";

        public TillSoilAction(Vector3Int gridLocation) : base(gridLocation)
        {
        }

        public TillSoilAction()
        {
        }
    }
}
