using UnityEngine;

namespace Assets.Scripts.Farmer.Backpack
{
    [CreateAssetMenu(fileName = "BackpackType", menuName = "Backpack/BackpackType", order = 0)]
    public class BackpackType : ScriptableObject
    {
        public int SlotCapacity;
        public int SeedCapacity;
    }
}