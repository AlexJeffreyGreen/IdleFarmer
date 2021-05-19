using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Scripts.Plants;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Utilities.TileManagement.Tiles
{
    [CreateAssetMenu(fileName = "LazyTile", menuName = "LazyTile", order = 0)]
    public class LazyTile : Tile
    {
        public int ACC;

        private List<Sprite> _sprites;

        private Seed _seed;
        //public SpriteCollection Sprites;
        public Seed Seed
        {
            get { return this._seed;}
            set
            {
                this._seed = value;
                this._sprites = this._seed.GetSprites();
            }
        }

        public void UpdateTile()
        {
            if (this.sprite != null)
            {
                this.sprite = this._sprites[ACC];
                this.ACC++;
            }
        }
        
    }
}