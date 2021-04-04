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

        public SeedDeserializer(TextAsset textFile)
        {
            this.textFile = textFile;
        }

        public SeedCollection DeserializationTest()
        {
            SeedCollection seeds = null;
            try
            {
                seeds = Newtonsoft.Json.JsonConvert.DeserializeObject<SeedCollection>(this.textFile.text);

            }
            catch(Exception e)
            {

            }
            return seeds;
        }
    }
}



