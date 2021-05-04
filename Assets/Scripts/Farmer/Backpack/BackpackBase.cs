using Assets.Scripts.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Search;

namespace Assets.Scripts.Farmer.Backpack
{
    public abstract class BackpackBase
    {
        //TODO implement factory for seeds??
        public abstract int MaxSeeds();
        private List<Seed> _seeds;

        public BackpackBase()
        {
            //_maxSeeds = 12; // currently hard coded. Different Backpacks will be able to store more seeds.
            _seeds = this.RetrieveSeeds();
        }

        public virtual List<Seed> RetrieveSeeds()
        {
            //TODO - read from save file
            if(_seeds == null)
            {
                //List<Seed> seedList = new List<Seed>();
                _seeds = new List<Seed>();
//                _seeds.AddRange(SeedManager.SeedManager.instance.SeedCollection.Seed); //TEST
                //return new List<Seed>();
            }
            return _seeds;
        }

        public virtual void AddSeedToBackpack(Seed seed)
        {
            _seeds.Add(seed);
        }

        public virtual void AddSeedToBackpack(List<Seed> seedList)
        {
            _seeds.AddRange(seedList);
        }

        public virtual IEnumerable<Seed> RetrieveSeedsByType(SeedType type, int Quantity)
        {
            return _seeds.Where(x => x.SeedType == type).Take(Quantity);
        }

        public virtual IEnumerable<Seed> RemoveSeed(SeedType type, int Quantity = 0)
        {
            IEnumerable<Seed> seedsOfType = _seeds.Where(t => t.SeedType == type);
            IEnumerable<Seed> seedsToRemove = null;
            
            if (seedsOfType.Count() < Quantity)
                Quantity = seedsOfType.Count();

            seedsOfType = seedsOfType.Take(Quantity);
            seedsToRemove = seedsOfType;

            _seeds = _seeds.Except(seedsOfType).ToList();
            
            return seedsToRemove;
        }
        
        

    }
}
