using Assets.Scripts.SmartTiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Plants
{
    public abstract class PlantBase : SmartTileBase
    {
        public abstract int GrowthTime { get;}
        public abstract int TileSize { get; }
        public abstract string Name { get; }
        public abstract double PricePerSeed { get; }
        public abstract double PricePerHarvest { get; }

        public PlantBase() { }
        public PlantBase(Vector3Int currentPostion, Tile tile) : base(currentPostion, tile) { }
    }
}
