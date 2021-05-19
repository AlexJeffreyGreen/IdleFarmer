using System;
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
        private void Awake()
        {
            if (instance == null)
                 instance = this;

            Instantiate(HighlightedSeed);
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
                
                //InventoryItem i = Instantiate(_marketItem, this._scrollView.transform, false);
            }    
        }

        private void Update()
        {
            
        }
    }
}