using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Utilities.TileManagement.Tiles
{
    public class TestTile : TileBase
    {
        public Sprite TestTileSprite;
        public GameObject TestGameObject;
        public Tile.ColliderType colliderType = Tile.ColliderType.Sprite;
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.gameObject = TestGameObject;
            tileData.colliderType = colliderType;
            tileData.sprite = TestTileSprite;
            tileData.flags = TileFlags.None;
            
            //base.GetTileData(position, tilemap, ref tileData);
        }
    }
}