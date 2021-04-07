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
    public class PlantBase : SmartTileBase
    {
        private Seed _seed;
        public virtual int GrowthTime { get { return this._seed.Gestation_Period; } }
        public virtual int TileSize { get { return this._seed.Tile_Size; } }
        public virtual string Name { get { return this._seed.Name; } }
        public virtual double PricePerSeed { get { return this._seed.Price_Per_Seed; } }
        public virtual double PricePerHarvest { get { return this._seed.Price_At_Harvest; } }
        public virtual double PercentOfLucky { get { return this._seed.Percentage_Of_Lucky; } }
        public virtual SeedType SeedType { get { return this._seed.SeedType; } }
        public virtual SpriteCollection Sprites { get { return this._seed.SpriteCollection; } }

        public PlantBase() { }
        public PlantBase(Vector3Int currentPostion, Tile tile) : base(currentPostion, tile) { }
        public PlantBase(Vector3Int currentPostion, Tile tile, Seed seed) : base(currentPostion, tile)
        {
            _seed = seed;
            this.RefreshTile();
        }

        public virtual void RefreshTile()
        {
            //TODO-Check if needed.
        }

        public virtual void UpdateSeed(Seed seed)
        {
            this._seed = seed;
            this.RefreshTile();
        }
    }




   


 

 

}
