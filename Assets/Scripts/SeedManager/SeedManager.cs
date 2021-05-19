using Assets.Scripts.Plants;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utilities.TileManagement.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.SeedManager
{
    public class SeedManager : MonoBehaviour
    {
        public static SeedManager instance = null;
        private static readonly object _lock = new object();
        //private TextAsset textFile; 
        private SeedDeserializer deseeder;// = new SeedDeserializer();
        private SeedCollection _seedCollection;
        public SeedCollection SeedCollection { get { return this._seedCollection; } }

        public List<Tile> allUITiles = new List<Tile>();
        public List<LazyTile> allSeedTiles = new List<LazyTile>();
        //public Tile GrassTileExample;
        
        SeedManager()
        {
            deseeder = new SeedDeserializer();
            _seedCollection = deseeder.DeserializeSeeds();
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != null)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            
            deseeder = new SeedDeserializer();
            _seedCollection = deseeder.DeserializeSeeds();
            this.GenerateTiles();

        }

        public void GenerateTiles()
        {
            string CurrentDirectroy = Directory.GetCurrentDirectory();

            foreach (UI_Tile uiTile in SeedCollection.UI_Tile)
            {
                //for (int i = 0; i < uiTile.SeedSprites.Length; i++)
                //{
                    Tile seedGestationTile = ScriptableObject.CreateInstance<LazyTile>(); // new Tile();
                    seedGestationTile.name = $"{uiTile.Name}";
                    seedGestationTile.sprite = uiTile.GetSprite();//seedGestationSprite;
                    allUITiles.Add(seedGestationTile);
                //}
            }

            foreach (Seed seed in SeedCollection.Seed)
            {
                for (int i = 0; i < seed.GetSprites().Count; i++)
                {
                    LazyTile seedGestationTile = ScriptableObject.CreateInstance<LazyTile>(); // new Tile();
                    seedGestationTile.name = $"{seed.Name}_{seed.GetSprites()[i]}";
                    seedGestationTile.sprite = seed.GetSprites()[i];//seedGestationSprite;
                    seedGestationTile.Seed = seed; 
                    allSeedTiles.Add(seedGestationTile);
                }
            }
        }
    }
}