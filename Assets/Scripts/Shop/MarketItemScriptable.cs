using Assets.Scripts.Plants;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    [CreateAssetMenu(fileName = "MarketItem", menuName = "Market/MarketItemScriptable", order = 0)]
    public class MarketItemScriptable : ScriptableObject
    {
        public Seed Seed;
        public int Quantity;
        public float Price;
    }
}