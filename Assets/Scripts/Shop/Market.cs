using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Farmer.Backpack;
using Assets.Scripts.Plants;
using Assets.Scripts.SeedManager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.Scripts.Shop
{
    public class Market : MonoBehaviour
    {
        public static Market instance;
        public HighlightedSeed HighlightedSeed;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private MarketItem _marketItem;
        private List<MarketItem> marketItems;
        private void Awake()
        {
            if (instance == null)
                 instance = this;

            Instantiate(HighlightedSeed);
            this.marketItems = new List<MarketItem>();
            // else if (instance != null)
            //     Destroy(gameObject);
            // DontDestroyOnLoad(gameObject);



        }

        private void Start()
        {
            foreach (Seed seed in SeedManager.SeedManager.instance.SeedCollection.Seed)
            {
                Debug.Log($"Making a market item for {seed.Name}");
                MarketItem i = Instantiate(_marketItem, this._gridLayoutGroup.transform);
                i.InitMarketItem(seed, 100, Convert.ToDecimal(seed.Price_Per_Seed));
                this.marketItems.Add(i);
                //InventoryItem i = Instantiate(_marketItem, this._scrollView.transform, false);
            }    
        }

        private void Update()
        {
            
        }

        public void UpdateMarketItem(Seed seed, int quantity)
        {
            MarketItem i = this.marketItems.FirstOrDefault(x => x.Seed == seed);
            if(i == null)
                return; //should not happen.
            i.UpdateMarketItem(quantity);
        }


        public MarketItem GetMarketItem(Seed seed)
        {
            MarketItem i = this.marketItems.FirstOrDefault(x => x.Seed == seed);
            if(i == null)
                return null; //should not happen.
            return i;
        }
    }
}