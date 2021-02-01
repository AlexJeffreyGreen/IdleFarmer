using Assets.Scripts.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    [CreateAssetMenu]
    public class Farm_Tile : Tile
    {
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);

            //FarmTile_GrassTile currentTile = // (FarmTile_GrassTile)this.gameObject;//as FarmTile_GrassTile;
        } 
    }
}
