using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Plants;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.SeedManager;
using Assets.Scripts.Utilities.TileManagement.Tiles;
using Assets.Scripts.Farmer.Action;
using Assets.Scripts.Farmer.Action.FarmingActions;

namespace Assets.Scripts.Utilities.TileManagement
{
    public class TileMapManager : MonoBehaviour
    {
        #region Singleton
        public static TileMapManager instance = null;
        #endregion

        [HideInInspector] public bool EnabledTileMap = true;

        [Header("Camera Properties")] 
        private Camera _mainCamera;

        [Header("Grid and Tilemaps")] 
        [SerializeField] private Grid _mainGrid;
        //[SerializeField] private Tile[] _tiles;
        private List<Tile> UI_Tiles;
        private List<LazyTile> Seed_Tiles;
        public Tile GrassTile;
        public Tile SelectionTile;
        public Tile SelectedTile;
        [SerializeField] private Tilemap[] _tileMapPrefabs;
        [SerializeField] private int xVar;
        [SerializeField] private int yVar;

        [SerializeField] private FarmableObject _farmableObject;
        //[SerializeField] private GameObject _farmableGameObject;
        [HideInInspector] public List<Tilemap> Tilemaps;
        public Dictionary<Vector3Int,FarmableObject> FarmableObjects;
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



            EnabledTileMap = true; // TODO - Dumb
            _mainCamera = Camera.main;
            Tilemaps = new List<Tilemap>();
            FarmableObjects = new Dictionary<Vector3Int, FarmableObject>();
            this.TilemapInstantiation();
            this.UI_Tiles = SeedManager.SeedManager.instance.allUITiles;
            this.Seed_Tiles = SeedManager.SeedManager.instance.allSeedTiles;
            this.GrassTile = this.UI_Tiles.First(x => x.name == "Grass");
            this.SelectionTile = this.UI_Tiles.First(x => x.name == "Selection");
            this.SelectedTile = this.UI_Tiles.First(x => x.name == "Selected");
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
            if (EnabledTileMap)
            {
                //DrawLocationAtMouse(this.Tilemaps[0]);
                Vector3Int locationAtMouse = LocationAtMouse(this.Tilemaps[0]);
                this.HighlightTileAtPosition(locationAtMouse, this.Tilemaps[0], this.Tilemaps[3]);

                if (Input.GetMouseButtonDown(0)
                    && this.Tilemaps[0].HasTile(locationAtMouse))
                {
                    Debug.Log($"Tile clicked. Selected tile {FarmerPlayer.instance.GetSelectedSeed()}");
                    //Tilemaps[Tilemaps.Count - 1].SetTile(locationAtMouse, SelectedTile);
                    //if (FarmerPlayer.instance.GetSelectedSeed() != null)
                    //    this.HandleLazyTileSelection(this.Tilemaps[2], locationAtMouse);
                    //TillSoilAction newBasicAction = ScriptableObject
                    //    .Instantiate<BasicAction>
                    //    (FarmingActionManager.instance.BasicFarmingAction);


                    IEnumerable<ActionBase> ActionsAtGridLocation = FarmingActionManager.instance.ActionsAtLocation(locationAtMouse);

                    if(ActionsAtGridLocation.Count() > 0)
                    {
                        foreach(ActionBase baseA in ActionsAtGridLocation)
                            Debug.Log(baseA.GetName());
                    }

                    TillSoilAction action = ActionFactory.Create<TillSoilAction>(locationAtMouse);
                    FarmingActionManager.instance.EnqueueNewAction(action);
                }
            }
        }


        private void HandleLazyTileSelection(Tilemap tM, Vector3Int newPosition)
        {
            FarmableObject currentFarmableObjectAtTile = null;
            Seed selectedSeed = FarmerPlayer.instance.GetSelectedSeed();

            if(selectedSeed == null)
                return;

            List<LazyTile> allPossibleTilesGivenSelectedSeed = this.Seed_Tiles.Where(x => x.Seed == selectedSeed).ToList();

            

            bool hasTileAtPosition = tM.HasTile(newPosition);
            bool hasFarmableObjectAtPosition = FarmableObjects.ContainsKey(newPosition);
            //TODO - seems weird.

            IEnumerable<Seed> seedsAvailable = 
                Inventory.instance.Items.Where(x=>x._InventoryItemScriptable != null)
                .Select(s=>s._InventoryItemScriptable)
                .Where(x => x.Seed != null && x.Seed == selectedSeed)
                .Select(z=>z.Seed);


      
            
            if (!seedsAvailable.Any())
            {
                Debug.Log($"Seed of type {selectedSeed.Name} is empty. You cannot add anymore.");
                return;
            }

            //till the soil
            this.Tilemaps[1].SetTile(newPosition, UI_Tiles.First(x=>x.name == "Tilled"));

            //Retrieve or create new farmable object
            if (!hasFarmableObjectAtPosition)
            {
                currentFarmableObjectAtTile = Instantiate(this._farmableObject);
                currentFarmableObjectAtTile.Position = newPosition; // redundant
                currentFarmableObjectAtTile.transform.SetParent(this.transform);
                currentFarmableObjectAtTile.Seed = selectedSeed;
                currentFarmableObjectAtTile.CurrentTiles = allPossibleTilesGivenSelectedSeed.ToArray();
                FarmableObjects.Add(newPosition, currentFarmableObjectAtTile);
            }
            else
                currentFarmableObjectAtTile = FarmableObjects[newPosition];
            

            //Add or Update Current Tile at position
            if (!hasTileAtPosition)
            {
                //tM.SetTile(newPosition, lazyT);
                tM.SetTile(newPosition,currentFarmableObjectAtTile.GetCurrentTile());
                Inventory.instance.RemoveInventoryItem(selectedSeed, 1);
                //FarmerPlayer.instance.Backpack.RemoveSeed(selectedSeed.SeedType, 1);
            }
            else
            {
                LazyTile t = tM.GetTile(newPosition) as LazyTile;
                if (t.Seed == currentFarmableObjectAtTile.Seed)
                {
                    tM.SetTile(newPosition, null);
                    LazyTile nextTile = currentFarmableObjectAtTile.GetNextTile();
                    if(nextTile != null)
                        tM.SetTile(newPosition, nextTile);
                }
                else
                {
                    Debug.Log("Wrong seed on current tile v. selected tile.");
                }
            }
            



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