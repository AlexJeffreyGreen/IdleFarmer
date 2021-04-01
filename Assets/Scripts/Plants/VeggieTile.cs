using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Plants
{
    public class VeggieTile : PlantBase
    {
        public override int GrowthTime => throw new NotImplementedException();

        public override int TileSize => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override double PricePerSeed => throw new NotImplementedException();

        public override double PricePerHarvest => throw new NotImplementedException();

        public VeggieTile() { }
        public VeggieTile(Vector3Int currentPostion, Tile tile) : base(currentPostion, tile) { }

    }
}
