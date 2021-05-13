using Assets.Scripts.Plants;
using UnityEngine;

namespace Market
{
    [CreateAssetMenu(fileName = "MarketItem", menuName = "Market/MarketItemScriptable", order = 0)]
    public class MarketItemScriptable : ScriptableObject
    {
        public Seed Seed;
        public int Quantity;
        public float Price;
    }
}