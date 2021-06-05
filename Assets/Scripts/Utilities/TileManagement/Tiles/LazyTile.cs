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
        private int dayCount;
        private int _gestationPeriod;
        private List<int> _gestationArray;
        private void CalculateGestationInformation()
        {
            this._gestationPeriod = this._seed.Gestation_Period;
            //TODO calculate gestations
            int temp = this._gestationPeriod / (this._seed.NumberOfSprites - 1);
            this._gestationArray = new List<int>();
            for (int i = 0; i < this._gestationPeriod; i += temp)
            {
                this._gestationArray.Add(i);
            }
        }
        
        private List<Sprite> _sprites;

        private Seed _seed;

        private bool _watered;
        //public SpriteCollection Sprites;
        public Seed Seed
        {
            get { return this._seed;}
            set
            {
                this._seed = value;
                this._sprites = this._seed.GetSprites();
                this.CalculateGestationInformation();
            }
        }

        public void UpdateTile()
        {
            if (this._seed != null)
            {
                if (_watered)
                {
                    if (this._gestationArray.Contains(this.dayCount))
                    {
                        if (this.sprite != null)
                        {
                            this.sprite = this._sprites[ACC];
                            this.ACC++;
                        }
                    }

                    this.dayCount++;
                    this.ToggleWatered();
                }
            }
        }

        public void ToggleWatered()
        {
            this._watered = !this._watered;
        }
        
    }
}