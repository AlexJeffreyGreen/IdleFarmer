using Assets.Scripts.Plants;
using UnityEngine;
using UnityEngine.UI;

namespace Market
{
    public class MarketItem : MonoBehaviour
    {
        private Seed _seed;

        private decimal _price_per_seed;

        private int _quantity;
        
        [SerializeField] private Button _marketButton;

        public void InitMarketItem(Seed seed, int quantity, decimal price_per)
        {
            this._seed = seed;
            this._quantity = quantity;
            this._price_per_seed = price_per;
        }

        public void HighlightSelectedSeedInUI()
        {
            
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