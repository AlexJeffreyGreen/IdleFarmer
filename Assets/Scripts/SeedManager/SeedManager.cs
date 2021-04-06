using Assets.Scripts.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SeedManager
{
    public sealed class SeedManager
    {
        private static SeedManager seedManager = null;
        private static readonly object _lock = new object();
        //private TextAsset textFile; 
        private SeedDeserializer deseeder;// = new SeedDeserializer();
        private SeedCollection _seedCollection;
        public SeedCollection SeedCollection { get { return this._seedCollection; } }

        SeedManager()
        {
            //textFile = Resources.Load<TextAsset>("//Assets//Scripts//Data//SeedData.json");
            deseeder = new SeedDeserializer();
            _seedCollection = deseeder.DeserializeSeeds();
        }

        public static SeedManager Manager
        {
            get
            {
                lock(_lock)
                {
                    if(seedManager == null)
                    {
                        seedManager = new SeedManager();
                    }
                    return seedManager;
                }
            }
        }
    }
}
