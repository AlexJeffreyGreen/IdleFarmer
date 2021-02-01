using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.SmartTiles
{
    public class BasicFarmTile : SmartTileBase
    {
        public BasicFarmTile(Vector3Int currentPostion, Tile tile)
        {
            this.Tile = tile;
            this.Position = currentPostion;
        }
    }
}
