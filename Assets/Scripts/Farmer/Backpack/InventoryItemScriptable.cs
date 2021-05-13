using Assets.Scripts.Plants;
using UnityEngine;

namespace Assets.Scripts.Farmer.Backpack
{
    [CreateAssetMenu(fileName = "InventoryItemScriptable", menuName = "Backpack/InventoryItemScriptable", order = 0)]
    public class InventoryItemScriptable : ScriptableObject
    {
        public Seed Seed;
        public int Quantity;
        public int Capacity;
    }
}