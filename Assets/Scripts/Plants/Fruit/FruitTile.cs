using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Plants.Fruit
{
    public class FruitTile : PlantBase
    {
        public override int GrowthTime => throw new NotImplementedException();

        public override int TileSize => throw new NotImplementedException();

        public override string Name => throw new NotImplementedException();

        public override double PricePerSeed => throw new NotImplementedException();

        public override double PricePerHarvest => throw new NotImplementedException();

        public override List<Sprite> GrowthSprites()
        {
            //Texture2D texture = Resources.Load<Sprite>("Sprites/")
            return new List<Sprite>()
            {
              //  Sprite.Create(new Texture2D())
            };
        }

        public FruitTile() { }
        public FruitTile(Vector3Int currentPostion, Tile tile) : base(currentPostion, tile) { }
    }
}
