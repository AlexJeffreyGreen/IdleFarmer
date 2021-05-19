using Assets.Scripts.Plants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class UI_Tile
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int NumberOfSprites { get; set; }
        private Sprite _sprite;

        public Sprite GetSprite()
        {
            if (_sprite == null)
                buildSprite();
            return _sprite;
        }

        private void buildSprite()
        {
            //_sprites = new List<Sprite>();
            //List<Sprite> returnValue = new List<Sprite>();
            
            string CurrentDirectroy = Directory.GetCurrentDirectory();
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

                this._sprite = seedGestationSprite;
            }
        }
    }
}
