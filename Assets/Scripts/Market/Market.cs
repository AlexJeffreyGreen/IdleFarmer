using System;
using Assets.Scripts.Farmer.Backpack;
using Assets.Scripts.Plants;
using Assets.Scripts.SeedManager;
using UnityEngine;
using UnityEngine.UIElements;

namespace Market
{
    public class Market : MonoBehaviour
    {
        [SerializeField] private ScrollView _scrollView;
        [SerializeField] private MarketItem _marketItem;
        private void Awake()
        {
            foreach (Seed seed in SeedManager.instance.SeedCollection.Seed)
            {
                //InventoryItem i = Instantiate(_marketItem, this._scrollView.transform, false);
            }    
        }

        private void Update()
        {
            
        }
    }
}