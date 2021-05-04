using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.Plants
{
    public class SeedSprite
    {
        public int Id { get; set; }
        public string Path { get; set; }
        
        private Sprite _sprite;
        
        public SeedSprite()
        {
            
        }
        
        public Sprite GetSprite()
        {
            if(this._sprite == null)
                this.BuildSprite();
            return this._sprite;
        }
        
        private void BuildSprite()
        {
            string CurrentDirectroy = Directory.GetCurrentDirectory();
            Texture2D seedGestationTexture = new Texture2D(2, 2);
            byte[] loadRaw =
                System.IO.File.ReadAllBytes(CurrentDirectroy + Path);
            seedGestationTexture.LoadImage(loadRaw);

            Rect seedGestationRect =
                new Rect(0.0f, 0.0f, seedGestationTexture.width, seedGestationTexture.height);
            Vector2 seedGestationVector2 = new Vector2(0.5f, 0.5f);//Vector2.zero;
            Sprite seedGestationSprite = Sprite.Create(seedGestationTexture, seedGestationRect,
                seedGestationVector2, 128.0f, 0, SpriteMeshType.Tight);
            this._sprite = seedGestationSprite;
        }
    }
}
