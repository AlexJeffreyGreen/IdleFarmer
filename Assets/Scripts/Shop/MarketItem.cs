using System;
using System.Linq;
using Assets.Scripts.Plants;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop
{
    public class MarketItem : MonoBehaviour
    {
        private Seed _seed;

        private decimal _price_per_seed;

        private int _quantity;
       // private Market _market;
        [SerializeField] private Button _marketButton;

        private void Awake()
        {
            
        }

        public void InitMarketItem(Seed seed, int quantity, decimal price_per)
        {
            this._seed = seed;
            this._quantity = quantity;
            this._price_per_seed = price_per;
            _marketButton.image.sprite = this._seed.GetSprites().Last();
        }

        public void HighlightSelectedSeedInUI()
        {
            Debug.Log($"Button pressed. Market Item - {_seed.Name}");
            Market.instance.HighlightedSeed.InitializeHighlightedSeed(_seed);
        }

        public void PurchaseSeed()
        {
            
        }
        
        public int Quantity()
        {
            return this._quantity;
        }

        public Seed Seed()
        {
            return this._seed;
        }

        public decimal Price()
        {
            return this._price_per_seed;
        }
    }
}