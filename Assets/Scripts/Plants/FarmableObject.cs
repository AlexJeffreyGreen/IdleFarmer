using System;
using System.Linq;
using Assets.Scripts.Utilities.TileManagement;
using Assets.Scripts.Utilities.TileManagement.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Plants
{
    public class FarmableObject : MonoBehaviour
    {
        public LazyTile[] CurrentTiles;


        public Vector3Int Position;
        private int ACC;

        private Seed _seed;

        private SpriteCollection _spriteCollection;

        public Seed Seed
        {
            get
            {
                return this._seed;
            }
            set
            {
                this._seed = value;
                //this._spriteCollection = this._seed.SpriteCollection;
            }
        }

        public LazyTile GetNextTile()
        {
            ACC++;
            if (ACC > this.CurrentTiles.Length - 2)
            {
                ACC = 0;
                return null;
            }

            LazyTile lazyTile = this.CurrentTiles[ACC];
            return lazyTile;
        }

        public LazyTile GetCurrentTile()
        {
            return this.CurrentTiles[ACC];
        }
        

        //public Sprite[] TestSprites;
       //public Sprite s;

        private void Awake()
        {
            //Debug.Log(Position);
            //Debug.Log("Hello world!");
            /*Tilemap tM = TileMapManager.instance.Tilemaps[0];
            Vector3Int locationAtMouse = TileMapManager.instance.LocationAtMouse(tM); 
            
            Tile t = TileMapManager.instance.GetTileAtMouse(tM);
            if (t != null)
            {
                if (t.sprite != null)
                    t.sprite = null; // = !t.sprite;
                else
                    t.sprite = s;
            }
            
            tM.RefreshTile(locationAtMouse);*/
        }

        private void Start()
        {
            Debug.Log(Position);
            
            // throw new NotImplementedException();
        }

        public void MouseClickedOnTile()
        {
            Debug.Log($"You have clicked at {Position}");
            //return this.RetrieveSprite();
            //throw new NotImplementedException();
        }
        

        private void Update()
        {
           
            //throw new NotImplementedException();
        }

        private void LateUpdate()
        {

        }
    }
}
