using System.Runtime.CompilerServices;
using Assets.Scripts.Plants;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Utilities.TileManagement
{
    public class LazyTileBase : Tile
    {
        
        public FarmableObject _farmableObject;
        private Vector3Int _position;
        
        
        public int GetRandomNumber()
        {
            return Random.Range(0, 1000);
        }

        public Vector3Int GetPosition()
        {
            return this._position;
        }

        public void Init(Vector3Int position)
        {
            this._position = position;
            Debug.Log($"Created Tile at position {position}");
        }
        
    }
}