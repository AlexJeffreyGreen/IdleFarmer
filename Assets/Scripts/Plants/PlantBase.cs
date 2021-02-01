using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Plants
{
    public abstract class PlantBase
    {
        //implement Factory<T> with Generics.
        //Strawberry strawberry = PlantBase.Create<Strawberry>();
        public abstract int GrowthTime { get;}
        public abstract int TileSize { get; }
        public abstract string Name { get; }
        public abstract double PricePerSeed { get; }
        public abstract double PricePerHarvest { get; }
    }
}
