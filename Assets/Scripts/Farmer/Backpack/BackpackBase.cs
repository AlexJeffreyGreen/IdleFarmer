﻿using Assets.Scripts.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Farmer.Backpack
{
    public abstract class BackpackBase
    {
        //TODO implement factory for seeds??
        public abstract int MaxSeeds();
        private List<Seed> _seeds;

        public Backpack()
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
                _seeds.AddRange(SeedManager.SeedManager.Manager.SeedCollection.Seed); //TEST
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

        public virtual Seed RetrieveSeed(SeedType type)
        {
            return _seeds.Where(x => x.SeedType == type).FirstOrDefault();
        }

    }
}