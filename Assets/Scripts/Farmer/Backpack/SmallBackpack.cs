using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Farmer.Backpack
{
    public class SmallBackpack : BackpackBase
    {
        private int _maxSeeds;

        public SmallBackpack() : base()
        {
            _maxSeeds = 12;
        }

        public override int MaxSeeds()
        {
            return _maxSeeds;
        }
    }
}
