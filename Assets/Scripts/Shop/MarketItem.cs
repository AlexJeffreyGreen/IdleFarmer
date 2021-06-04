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
        [SerializeField] private Text _marketItemQuantityText;

        private void Awake()
        {
            
        }

        public void InitMarketItem(Seed seed, int quantity, decimal price_per)
        {
            this._seed = seed;
            this._quantity = quantity;
            this._price_per_seed = price_per;
            this._marketItemQuantityText.text = this._quantity.ToString();
            _marketButton.image.sprite = this._seed.GetSprites().Last();
        }

        public void UpdateMarketItem(int quantity)
        {
            this._quantity += quantity;
            if(this._quantity < 0)
                this._quantity = 0;
            this._marketItemQuantityText.text = this._quantity.ToString();
        }

        public void HighlightSelectedSeedInUI()
        {
            Debug.Log($"Button pressed. Market Item - {_seed.Name}");
            Market.instance.HighlightedSeed.InitializeHighlightedSeed(_seed);
        }

        public void PurchaseSeed()
        {
            
        }
        
        public int Quantity
        {
            get { return this._quantity; }
        }

        public Seed Seed
        {
            get { return this._seed; }
        }

        public decimal Price
        {
            get { return this._price_per_seed; }
        }
    }
}