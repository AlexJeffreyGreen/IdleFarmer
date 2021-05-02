using Assets.Scripts.Plants;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public List<Tile> allSeedTiles = new List<Tile>();
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

        //public void DeserializeSeeds()
       // {
        //    _seedCollection = deseeder.DeserializeSeeds();
        //}
        

        public void GenerateTiles()
        {
            //List<Tile> allTiles = Resources.LoadAll<Tile>("Pallettes").ToList(); //hard coded tiles seem weird.

            string CurrentDirectroy = Directory.GetCurrentDirectory();

            foreach (UI_Tile uiTile in SeedCollection.UI_Tile)
            {
                for (int i = 0; i < uiTile.SeedSprites.Length; i++)
                {
                    Texture2D seedGestationTexture = new Texture2D(2, 2);
                    byte[] loadRaw = System.IO.File.ReadAllBytes(CurrentDirectroy + uiTile.SeedSprites[i].Path);
                    seedGestationTexture.LoadImage(loadRaw);

                    Rect seedGestationRect =
                        new Rect(0, 0, seedGestationTexture.width, seedGestationTexture.height);
                    //seedGestationRect.center = Vector2.zero;
                    //Vector2 seedGestationVector2 = seedGestationRect.center;

                    Vector2 seedGestationVector2 = new Vector2(0.5f, 0.5f);//Vector2.zero;
                    Sprite seedGestationSprite = Sprite.Create(seedGestationTexture, seedGestationRect,
                        seedGestationVector2, 128.0f, 0, SpriteMeshType.Tight); // load sprite based on location in path?
                    
                    Tile seedGestationTile = ScriptableObject.CreateInstance<Tile>(); // new Tile();
                    seedGestationTile.name = $"{uiTile.Name}";
                    seedGestationTile.sprite = seedGestationSprite;
                    allUITiles.Add(seedGestationTile);
                }
            }

            foreach (Seed seed in SeedCollection.Seed)
            {
                for (int i = 0; i < seed.SpriteCollection.SeedSprites.Length; i++)
                {
                    Texture2D seedGestationTexture = new Texture2D(2, 2);
                    byte[] loadRaw =
                        System.IO.File.ReadAllBytes(CurrentDirectroy + seed.SpriteCollection.SeedSprites[i].Path);
                    seedGestationTexture.LoadImage(loadRaw);

                    Rect seedGestationRect =
                        new Rect(0.0f, 0.0f, seedGestationTexture.width, seedGestationTexture.height);
                    Vector2 seedGestationVector2 = new Vector2(0.5f, 0.5f);//Vector2.zero;
                    Sprite seedGestationSprite = Sprite.Create(seedGestationTexture, seedGestationRect,
                        seedGestationVector2, 128.0f, 0, SpriteMeshType.Tight); // load sprite based on location in path?

                    Tile seedGestationTile = ScriptableObject.CreateInstance<Tile>(); // new Tile();
                    seedGestationTile.name = $"{seed.Name}_{seed.SpriteCollection.SeedSprites[i]}";
                    seedGestationTile.sprite = seedGestationSprite;
                    allSeedTiles.Add(seedGestationTile);
                }
            }
        }

        
        

      


    }
}