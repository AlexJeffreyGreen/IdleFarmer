using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Plants;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.SeedManager;


namespace Assets.Scripts.Utilities.TileManagement
{
    public class TileMapManager : MonoBehaviour
    {
        #region Singleton
        public static TileMapManager instance = null;
        #endregion

        [Header("Camera Properties")] 
        private Camera _mainCamera;

        [Header("Grid and Tilemaps")] 
        [SerializeField] private Grid _mainGrid;
        //[SerializeField] private Tile[] _tiles;
        private List<Tile> UI_Tiles;
        private List<Tile> Seed_Tiles;
        public Tile GrassTile;
        public Tile SelectionTile;
        public Tile SelectedTile;
        [SerializeField] private Tilemap[] _tileMapPrefabs;
        [SerializeField] private int xVar;
        [SerializeField] private int yVar;

        [SerializeField] private FarmableObject _farmableObject;
        //[SerializeField] private GameObject _farmableGameObject;
        [HideInInspector] public List<Tilemap> Tilemaps;
        private Vector3Int _previousPosition;
        
        [Header("Camera and Offsets")] 
        [SerializeField] private float _mainCameraOffset_X;

        [SerializeField] private float _mainCameraOffset_Y;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != null)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            
            
            

            _mainCamera = Camera.main;
            Tilemaps = new List<Tilemap>();
            this.TilemapInstantiation();
            this.UI_Tiles = SeedManager.SeedManager.instance.allUITiles;
            this.Seed_Tiles = SeedManager.SeedManager.instance.allSeedTiles;
            //this.GrassTile = this.UI_Tiles.First(x => x.name == "Grass");
            //this.SelectionTile = this.UI_Tiles.First(x => x.name == "Selection");
            //this.SelectedTile = this.UI_Tiles.First(x => x.name == "Selected");
            _previousPosition = new Vector3Int(-1, -1, 0);

            //throw new NotImplementedException();
        }

       
        
        private void Start()
        {
            this.GenerateTileMapWithSingleTile(Tilemaps[0], GrassTile);
            this._mainCamera.transform.position = new Vector3(_mainCameraOffset_X, _mainCameraOffset_Y, _mainCamera.transform.position.z);
        }
        private void TilemapInstantiation()
        {
            Grid mainGrid = Instantiate(_mainGrid);
            foreach (Tilemap tilemap in _tileMapPrefabs)
            {
                Tilemap newInstanceOfMap = Instantiate(tilemap); 
                newInstanceOfMap.ClearAllTiles();
                Tilemaps.Add(newInstanceOfMap);
                newInstanceOfMap.transform.SetParent(mainGrid.transform);
            }
        }

        private void GenerateTileMapWithSingleTile(Tilemap tM, Tile t)
        {
            //List<Vector3Int> GridBoudries = new List<Vector3Int>();
            for(int x = 0; x < xVar; x++)
            for (int y = 0; y < yVar; y++)
            {
                Vector3Int newPosition = new Vector3Int(x, y, 0);

          
                tM.SetTile(newPosition, t);
                //tM.SetTile(new Vector3Int(x,y, 0), t);
            }
        }


        private void Update()
        {
            //DrawLocationAtMouse(this.Tilemaps[0]);
            Vector3Int locationAtMouse = LocationAtMouse(this.Tilemaps[0]);
            this.HighlightTileAtPosition(locationAtMouse, this.Tilemaps[0], this.Tilemaps[1]);

            if (Input.GetMouseButtonDown(0)
                && this.Tilemaps[0].HasTile(locationAtMouse))
            {
                Tilemaps[Tilemaps.Count - 1].SetTile(locationAtMouse, SelectedTile);
                this.HandleLazyTileSelection(this.Tilemaps[this.Tilemaps.Count - 1], locationAtMouse);
            }
        }

        private void HandleLazyTileSelection(Tilemap tM, Vector3Int newPosition)
        {
            //TOOD - Prevent duplication if tilemap already has a lazy tile at position
            LazyTileBase b = ScriptableObject.CreateInstance<LazyTileBase>();
            b.sprite = this.SelectedTile.sprite;//this._tiles[0].sprite;
            b._farmableObject = Instantiate(this._farmableObject);
            b._farmableObject.transform.SetParent(this.transform);
            b._farmableObject.Position = newPosition;
            b.Init(newPosition);
            tM.SetTile(newPosition, b);
        }
        
        public void HighlightTileAtPosition(Vector3Int locationAtMouse, Tilemap mainMap, Tilemap highLightMap)
        {
            if (mainMap.HasTile(locationAtMouse))
            {
                if (locationAtMouse != _previousPosition)
                {
                    //previousPosition = locationAtMouse;
                    highLightMap.SetTile(_previousPosition, null);
                    _previousPosition = locationAtMouse;
                }

                highLightMap.SetTile(locationAtMouse, SelectionTile);
            }
            else
            {
                highLightMap.SetTile(_previousPosition, null);
            }
        }
        
        public Vector3Int LocationAtMouse(Tilemap tileMap)
        {
            Vector3 worldPositon = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPositon.z = 0;

            Vector3Int currentCell = tileMap.WorldToCell(worldPositon);
            return currentCell;
        }

        public Tile GetTileAtMouse(Tilemap tileMap)
        {
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = tileMap.tileAnchor.z;
            Vector3Int currentCellPosition = tileMap.WorldToCell(worldPosition);
            if (tileMap.HasTile(currentCellPosition))
                return tileMap.GetTile(currentCellPosition) as Tile;
            return null;
        }

        public Tile GetTileAtPosition(Tilemap tileMap, Vector3 position)
        {
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(position);
            worldPosition.z = tileMap.tileAnchor.z;
            Vector3Int currentCellPosition = tileMap.WorldToCell(worldPosition);
            if (tileMap.HasTile(currentCellPosition))
                return tileMap.GetTile(currentCellPosition) as Tile;
            return null;
        }

        public List<Tile> GetAllTilesAtPosition(Vector3 position)
        {
            List<Tile> returnValues = new List<Tile>();
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(position);

            foreach (Tilemap t in Tilemaps)
            {
                worldPosition.z = t.tileAnchor.z;
                Vector3Int currentCellPosition = t.WorldToCell(worldPosition);
                if (t.HasTile(currentCellPosition))
                    returnValues.Add(t.GetTile(currentCellPosition) as Tile);
                return null;
            }

            return returnValues;
        }

    }
}