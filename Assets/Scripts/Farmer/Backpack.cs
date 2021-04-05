using Assets.Scripts.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Farmer
{
    public class Backpack
    {
        private int _maxSeeds;
        private List<Seed> _seeds;

        public Backpack()
        {
            _maxSeeds = 12; // currently hard coded. Different Backpacks will be able to store more seeds.
            _seeds = this.RetrieveSeeds();
        }

        private List<Seed> RetrieveSeeds()
        {
            //TODO - read from save file
            return new List<Seed>();
        }

        public void AddSeedToBackpack(Seed seed)
        {
            _seeds.Add(seed);
        }

        public void AddSeedToBackpack(List<Seed> seedList)
        {
            _seeds.AddRange(seedList);
        }

        public IEnumerable<Seed> RetrieveSeedsByType(SeedType type, int Quantity)
        {
            return _seeds.Where(x => x.SeedType == type).Take(Quantity);
        }

        public Seed RetrieveSeed(SeedType type)
        {
            return _seeds.Where(x => x.SeedType == type).FirstOrDefault();
        }
    }
}
