using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Plants
{
    public class Seed
    {
        //public SpriteCollection SpriteCollection { get; set; }
        public int Gestation_Period { get; set; }
        public float Price_Per_Seed { get; set; }
        public float Price_At_Harvest { get; set; }
        public float Percentage_Of_Lucky { get; set; }
        public int Tile_Size { get; set; }
        public SeedType SeedType { get; set; }
        public string Path { get; set; }
        public int NumberOfSprites { get; set; }

        private List<Sprite> _sprites;

        public List<Sprite> GetSprites()
        {
            if (_sprites == null)
                buildSprites();
            return this._sprites;
        }
        
        public string Name { get; set; }

        private void buildSprites()
        {
            _sprites = new List<Sprite>();
            //List<Sprite> returnValue = new List<Sprite>();
            
            string CurrentDirectroy = Directory.GetCurrentDirectory(); //Use unity library to do this instead
            Texture2D seedGestationTexture = new Texture2D(2, 2);
            byte[] loadRaw =
                System.IO.File.ReadAllBytes(CurrentDirectroy + Path);
            seedGestationTexture.LoadImage(loadRaw);
            

          //  Rect seedGestationRect =
          //      new Rect(0.0f, 0.0f, seedGestationTexture.width / this.numberOfSprites, seedGestationTexture.height);

          int width = 128;//seedGestationTexture.width / this.NumberOfSprites;
          int height = seedGestationTexture.height;
          
          
          
            Vector2 seedGestationVector2 = new Vector2(0.5f, 0.5f);//Vector2.zero;

            for (int i = 0; i < this.NumberOfSprites; i++)
            {

                Sprite seedGestationSprite = Sprite.Create(seedGestationTexture,
                    new Rect(i * width, 0, width, height),
                    seedGestationVector2,
                    128.0f,
                    0,
                    SpriteMeshType.Tight);
                
                this._sprites.Add(seedGestationSprite);
            }
        }
    }


}
