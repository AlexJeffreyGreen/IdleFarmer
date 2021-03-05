using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.SmartTiles
{
    public class GrassTile : SmartTileBase
    {
        public GrassTile() { }
        public GrassTile(Vector3Int currentPostion, Tile tile) : base(currentPostion, tile) { }
    }
}
