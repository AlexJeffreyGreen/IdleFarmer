using Assets.Scripts.Plants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SeedManager
{
    public class SeedDeserializer
    {
        public Dictionary<Guid, PlantBase> PlantObjects = new Dictionary<Guid, PlantBase>();
        private TextAsset textFile;

        public SeedDeserializer()
        {
            //textFile = text;
        }

        public SeedCollection DeserializeSeeds()
        {
            SeedCollection seeds = null;
            try
            {
                using(StreamReader r = new StreamReader("Assets//Scripts//Data//SeedData.json"))
                {
                    string json = r.ReadToEnd();
                    seeds = Newtonsoft.Json.JsonConvert.DeserializeObject<SeedCollection>(json);
                }
            }
            catch(Exception e)
            {

            }
            return seeds;
        }
    }
}



