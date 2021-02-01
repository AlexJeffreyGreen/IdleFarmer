using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.SmartTiles
{
    public abstract class SmartTileBase
    {
        private Vector3Int _position;
        protected Vector3Int Position { get { return this._position; } set { this._position = value; } }
        private Tile _tile;
        protected Tile Tile { get { return this._tile; } set { this._tile = value; } }

        public SmartTileBase()
        {

        }

        public Tile GetTile() { return this._tile; }
        public Tile SetTile(Tile newTile) { this._tile = newTile; return this._tile; }
        public Vector3Int GetPosition() { return this._position; }
        
    }
}
