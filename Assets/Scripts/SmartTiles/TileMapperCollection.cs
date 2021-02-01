using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.SmartTiles
{
    public class TileMapperCollection
    {
        public Dictionary<Guid, SmartTileBase> TileCollection = new Dictionary<Guid, SmartTileBase>();
        
        public TileMapperCollection() { }

        public KeyValuePair<Guid, SmartTileBase> GetTileAtPosition(Vector3Int position)
        {
            foreach(KeyValuePair<Guid, SmartTileBase> pairing in TileCollection)
            {
                if(pairing.Value.GetPosition() == position)
                    return pairing;
            }
            return new KeyValuePair<Guid, SmartTileBase>(Guid.Empty, null);
        }
    }
}
