using Assets.Scripts.Plants;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

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
                    return seedManager ?? (seedManager = new SeedManager());
                }
            }
        }

        public Tile[] RetrieveAllTiles()
        {
            //List<Tile> allTiles = Resources.LoadAll<Tile>("Pallettes").ToList(); //hard coded tiles seem weird.
            List<Tile> allTiles = new List<Tile>();
            string CurrentDirectroy = Directory.GetCurrentDirectory();

            foreach(UI_Tile uiTile in SeedCollection.UI_Tile)
            {
                for(int i = 0; i < uiTile.SeedSprites.Length; i++)
                {
                    Texture2D seedGestationTexture = new Texture2D(128, 128);
                    byte[] loadRaw = System.IO.File.ReadAllBytes(CurrentDirectroy + uiTile.SeedSprites[i].Path);
                    seedGestationTexture.LoadImage(loadRaw);

                    Rect seedGestationRect = new Rect(0.0f, 0.0f, seedGestationTexture.width, seedGestationTexture.height);
                    Vector2 seedGestationVector2 = new Vector2(0.0f, 0.0f);
                    Sprite seedGestationSprite = Sprite.Create(seedGestationTexture, seedGestationRect, seedGestationVector2, 128.0f); // load sprite based on location in path?

                    Tile seedGestationTile = ScriptableObject.CreateInstance<Tile>();// new Tile();
                    seedGestationTile.name = $"{uiTile.Name}";
                    seedGestationTile.sprite = seedGestationSprite;
                    allTiles.Add(seedGestationTile);
                }
            }

            foreach(Seed seed in SeedCollection.Seed)
            {
                for(int i = 0; i < seed.SpriteCollection.SeedSprites.Length; i++)
                {
                    Texture2D seedGestationTexture = new Texture2D(128, 128);
                    byte[] loadRaw = System.IO.File.ReadAllBytes(CurrentDirectroy + seed.SpriteCollection.SeedSprites[i].Path);
                    seedGestationTexture.LoadImage(loadRaw);

                    Rect seedGestationRect = new Rect(0.0f, 0.0f, seedGestationTexture.width, seedGestationTexture.height);
                    Vector2 seedGestationVector2 = new Vector2(0.0f, 0.0f);
                    Sprite seedGestationSprite = Sprite.Create(seedGestationTexture, seedGestationRect, seedGestationVector2, 128.0f); // load sprite based on location in path?

                    Tile seedGestationTile = ScriptableObject.CreateInstance<Tile>();// new Tile();
                    seedGestationTile.name = $"{seed.Name}_{seed.SpriteCollection.SeedSprites[i]}";
                    seedGestationTile.sprite = seedGestationSprite;
                    allTiles.Add(seedGestationTile);
                }
            }

            return allTiles.ToArray();
            //throw new NotImplementedException();
        }

        public void GetTileAtMouseButton()
        {
            
        }
    }
}