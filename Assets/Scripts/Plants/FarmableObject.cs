using System;
using System.Linq;
using Assets.Scripts.Utilities.TileManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Plants
{
    public class FarmableObject : MonoBehaviour
    {
        public Vector3Int Position;
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

        private void Update()
        {
           
            //throw new NotImplementedException();
        }

        private void LateUpdate()
        {

        }
    }
}
