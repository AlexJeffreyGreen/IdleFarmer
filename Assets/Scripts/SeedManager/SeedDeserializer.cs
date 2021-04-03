using Assets.Scripts.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SeedManager
{
    public class SeedDeserializer
    {
        public Dictionary<Guid, PlantBase> PlantObjects = new Dictionary<Guid, PlantBase>();


        //TODO - Seed Deserialization process. Remove all classes for specific seeds.
        //All seeds to be stored in json format to be parsed by anon class.
    }
}
