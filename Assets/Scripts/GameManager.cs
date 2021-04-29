//using Assets.Scripts;
//using Assets.Scripts;
using Assets.Scripts;
using Assets.Scripts.Plants;
using Assets.Scripts.SeedManager;
using Assets.Scripts.SmartTiles;
//using Assets.Scripts.Tiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Farmer.Action;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    
    
    #region Unity Inspector Properties

    [Header("Grid Properties")]
    [SerializeField] private Grid MainGrid;
    [SerializeField] private List<Tilemap> tileMapPrefabs;
    private List<Tilemap> Tilemaps;
    //private Grid MainGrid;
    [SerializeField] private Tile MainGrassTile;
    [SerializeField] private Tile MainSelectorTile;
    [SerializeField] private Tile MainSelectionTile;
    [SerializeField] private int xVar;
    [SerializeField] private int yVar;
    private Vector3Int previousPosition;

    [Header("Camera Properties")] 
    private Camera _mainCamera;
    [SerializeField] private float _mainCameraOffset_X;
    [SerializeField] private float _mainCameraOffset_Y;
    
    #region Mouse Positioning Fields and Properties

    private Vector3Int PreviousMousePosition;
    private Vector3Int CurrentMousePosition;

    public Vector3Int GetCurrentPosition()
    {
        return this.CurrentMousePosition;
    }
    public Vector3Int GetPreviousPosition()
    {
        return this.PreviousMousePosition;
    }

    #endregion
    #endregion
    
    
    
    #region Unity Functions
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        _mainCamera = Camera.main;
        //this.MainGrid = Instantiate(mainGridPrefab); // the original method
        Grid mG = Instantiate(MainGrid);
        //Instantiate(mainGridPrefab);
        Tilemaps = new List<Tilemap>();
        
        foreach (Tilemap tilemap in tileMapPrefabs)
        {
            Tilemap newInstanceOfMap = Instantiate(tilemap); 
            newInstanceOfMap.ClearAllTiles();
            Tilemaps.Add(newInstanceOfMap);
            newInstanceOfMap.transform.SetParent(mG.transform);
            //newInstanceOfMap.transform.SetParent(mainGridPrefab.transform);
            //newInstanceOfMap.transform.SetParent(MainGrid.transform); // the original method
        }
        // this.Tilemaps = mainGrid.GetComponentsInChildren<Tilemap>();
       //this.Tilemaps = new List<Tilemap>();
       // for (int i = 0; i < mainGrid.transform.childCount; i++)
       // {
            //GameObject tileMap = Instantiate(mainGrid.transform.GetChild(i).gameObject);
            //tileMap.transform.SetParent(mainGrid.transform);
       //     this.Tilemaps.Add(mainGrid.transform.GetChild(i).GetComponent<Tilemap>());
       //     this.Tilemaps[i].gameObject.SetActive(true);
            //Instantiate(this.Tilemaps[i]);
       // } //this.Tilemaps.Add(Instantiate(mainGrid.transform.GetChild(i).GetComponent(gameObject)));

        //    Instantiate(t);
        previousPosition = new Vector3Int(-1, -1, 0);
        //throw new NotImplementedException();
    }

    private void Start()
    {
        List<Vector3Int> GridBoundries = new List<Vector3Int>();
        List<TileBase> GrassTiles = new List<TileBase>();
        
        for(int x = 0; x < xVar; x++)
        {
            for(int y = 0; y < xVar; y++)
            {
                Tilemaps[0].SetTile(new Vector3Int(x, y, 0), MainGrassTile);
                //GridBoundries.Add(new Vector3Int(x, y, 0));
                //GrassTiles.Add(this.MainGrassTile);
            }
        }
        
        
        
        //Tilemaps[0].SetTiles(GridBoundries.ToArray(), GrassTiles.ToArray());

        _mainCamera.transform.position = new Vector3(_mainCameraOffset_X, _mainCameraOffset_Y, _mainCamera.transform.position.z);
    }

    private void Update()
    {
        //DrawLocationAtMouse(this.Tilemaps[0]);
        Vector3Int locationAtMouse = LocationAtMouse(this.Tilemaps[0]);
        this.HighlightTileAtPosition(locationAtMouse, this.Tilemaps[0], this.Tilemaps[1]);

        if (Input.GetMouseButtonDown(0)
            && this.Tilemaps[0].HasTile(locationAtMouse))
        {
            Tilemaps[Tilemaps.Count-1].SetTile(locationAtMouse, MainSelectionTile);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            this.SaveAndExit();
        }
    }

    public void HighlightTileAtPosition(Vector3Int locationAtMouse, Tilemap mainMap, Tilemap highLightMap)
    {
        if (mainMap.HasTile(locationAtMouse))
        {
            if (locationAtMouse != previousPosition)
            {
                //previousPosition = locationAtMouse;
                highLightMap.SetTile(previousPosition, null);
                previousPosition = locationAtMouse;
            }

            highLightMap.SetTile(locationAtMouse, this.MainSelectorTile);
        }
        else
        {
            highLightMap.SetTile(previousPosition, null);
        }
    }


    public Vector3Int LocationAtMouse(Tilemap tileMap)
    {
        Vector3 worldPositon = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPositon.z = 0;

        Vector3Int currentCell = tileMap.WorldToCell(worldPositon);
        return currentCell;
    }

    public TileBase[] TilesAtMouseLocation()
    {
        List<TileBase> ReturnTiles = new List<TileBase>();
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        worldPosition.z = 0;

        foreach (Tilemap tileMap in Tilemaps)
        {
            Vector3Int currentCell = tileMap.WorldToCell(new Vector3(worldPosition.x, worldPosition.y, worldPosition.z + tileMap.tileAnchor.z));
            if(tileMap.HasTile(currentCell))
                ReturnTiles.Add(tileMap.GetTile(currentCell));
        }

        return ReturnTiles.ToArray();
    }

    private void LateUpdate()
    {
        //throw new NotImplementedException();
    }
    #endregion

    #region Mouse Movement Handlers

    public bool IsMouseOverTile()
    {
        return false;
    }
    #endregion
    
    #region Action Queue Management

    public void SpendActionButtonClick()
    {
        StartCoroutine("DumpActionQueue");
    }

    IEnumerator DumpActionQueue()
    {
        while (ActionQueue.ActionQueueManager.Count > 0)
        {
            // ActionBase action = ActionQueue.ActionQueueManager.DequeueAction();
            // TileMaps[TileMapType.TILLED.ToInt()].SetTile(action.GetPosition(), null);
            // this.Player1.FarmingActionTaken(action);
            //
            // //GrassTile newTile = SmartTileFactory.Create<GrassTile>(CurrentMousePostion, Tiles[TileType.GRASS.ToInt()]);
            // //TileMaps[TileMapType.GRASS.ToInt()].SetTile(action.GetPosition(), null);
            //
            //
            yield return new WaitForSeconds(1);
        }
    }

    #endregion

    #region Application Menu Actions

    public void SaveAndExit(bool save = true)
    {
        if (save)
            if (!this.ExecuteSaveRequest())
                throw new Exception("Save error.");
            else
                Debug.Log("Saved.");
        Application.Quit();
    }

    public bool ExecuteSaveRequest()
    {
        return true;
    }

    #endregion
    
    /*
    public Text DayText;
    public int DayCount;
    public bool completedDay = true;
    //TILE COLLECTION
    private TileMapperCollection _farmGrassTiles;
    public TileMapperCollection FarmGrassTiles { get { return this._farmGrassTiles; } }

    private TileMapperCollection _seedlingTiles;
    public TileMapperCollection SeedingTiles { get { return this._seedlingTiles; } }

    public Tile[] Tiles;
    public Tilemap[] TileMaps;

    //CAMERA AND CANVAS
    public Camera _mainCamera;
    public Canvas MarketCanvas;
    public Canvas SeedsAndPlantsCanvas;
    public Canvas MainButtonCanvas;

    public Image MainTileImage;
    public Image SeedTileImage;

    //GRID SIZING
    public int xVar;
    public int yVar;
   
    //CURRENT GRID POSITION
    private Vector3Int _currentPos;
    public Vector3Int GetCurrentPos
    {
        get { return this._currentPos; }
    }
    public Vector3Int CurrentPosition { get { return this._currentPos; } }

    public Vector3Int CurrentMousePostion;
    //EXTRAS
    public System.Random Random;
    public TileType TileType;

    //Text
    public TextAsset SeedJson;

    //Farmer Player
    public FarmerPlayer Player1; //TODO - Dream to implement lobby and multiplayer
    //public StaminaBar StaminaBar;
    //public int MaxStamina = 100;
    //public int CurrentStamina;
    
    public float CameraOffset;

    private void Awake()
    {

        //SeedDeserializer deserializer = new SeedDeserializer(SeedJson);
        //SeedCollection currentCollection = deserializer.DeserializationTest();
        //this.Player1 = this.Player1.GetComponent<FarmerPlayer>();

        //Debug.Log(Player1.ToString());

        this.Tiles  = SeedManager.Manager.RetrieveAllTiles();

        if(DayCount == 0)
            DayCount++;

        this.DayText = this.DayText.GetComponent<Text>();
        IncrementDay();

        this._currentPos = new Vector3Int();

        System.Random rand = new System.Random();

        this.MarketCanvas = this.MarketCanvas.GetComponent<Canvas>();
        this.MarketCanvas.enabled = false;
        this.SeedsAndPlantsCanvas = this.SeedsAndPlantsCanvas.GetComponent<Canvas>();
        this.SeedsAndPlantsCanvas.enabled = false;

        this._farmGrassTiles = new TileMapperCollection();
        this._seedlingTiles = new TileMapperCollection();

        //Deserialize seed collection
        //build up seed bag

        //CurrentStamina = MaxStamina;
        //StaminaBar.SetMaxStamina(CurrentStamina);
    }

    private void IncrementDay()
    {
        this.DayText.text = $"Day: {DayCount}";
    }

    void Start()
    {

        for(int x = 0; x < xVar; x++)
        {
            for(int y = 0; y < xVar; y++)
            {
                CreateNewTileAt(TileType.GRASS, x, y);
            }
        }

        _mainCamera.transform.position = new Vector3(0, 2.5f, _mainCamera.transform.position.z);

        //InvokeRepeating("CheckTilesDaily", 2.0f, 15.0f);
    }

    private void CreateNewTileAt(TileType type, int x, int y, int z = 0)
    {
        Vector3Int p = new Vector3Int(x, y, z);

        if(type == TileType.GRASS)
        {
            GrassTile newTile = SmartTileFactory.Create<GrassTile>(p, Tiles[TileType.GRASS.ToInt()]);

            this._farmGrassTiles.TileCollection.Add(Guid.NewGuid(),newTile);

            TileMaps[TileMapType.GRASS.ToInt()].SetTile(p, newTile.GetTile());
        }
    }

    void Update()
    {
        if(this.MarketCanvas.enabled || this.SeedsAndPlantsCanvas.enabled)
        {
            MenuProcessing();
        }
        else
        {
            TileMovement();
            
            
        }
    }

 
        
    private void LateUpdate()
    {
        
    }

    public bool isMouseOverTile()
    {
        Vector3 tileLocation = Input.mousePosition;
        Ray screenWorldToRay = Camera.main.ScreenPointToRay(tileLocation);
        Vector3 worldPoint = screenWorldToRay.GetPoint(-screenWorldToRay.origin.z / screenWorldToRay.direction.z);
        worldPoint = new Vector3(worldPoint.x - CameraOffset, worldPoint.y - CameraOffset, worldPoint.z);
        CurrentMousePostion = TileMaps[TileMapType.GRASS.ToInt()].WorldToCell(worldPoint);
        return TileMaps[0].HasTile(CurrentMousePostion);
    }
    
    private void TileMovement()
    {
        if(isMouseOverTile())
        {

            ManageSelectedTileAtPosition(CurrentMousePostion);
            if (Input.GetMouseButtonDown(0))
            {
                GrassTile newTile = SmartTileFactory.Create<GrassTile>(CurrentMousePostion, Tiles[TileType.GRASSHIGHLIGHTED.ToInt()]);
                TileMaps[TileMapType.TILLED.ToInt()].SetTile(CurrentMousePostion, null);
                TileMaps[TileMapType.TILLED.ToInt()].SetTile(CurrentMousePostion, newTile.GetTile());
            }


            // ManageCanvasMainTile(gridPos);
        }
        else
        {
            //We should temp store the previous tile position so it looks less janky
            //TileMaps[TileMapType.SELECTOR.ToInt()].ClearAllTiles();
        }
    }

    public void HandleTileClick(Vector3Int clickedPosition)
    {
        if (!TileMaps[TileMapType.TILLED.ToInt()].HasTile(clickedPosition))
        {
            Debug.Log($"Adding Tile at Position... {clickedPosition}");

            GrassTile tile = SmartTileFactory.Create<GrassTile>(clickedPosition, Tiles[TileType.TILLED.ToInt()]);
            this.SeedingTiles.TileCollection.Add(Guid.NewGuid(), tile);
            TileMaps[TileMapType.TILLED.ToInt()].SetTile(clickedPosition, tile.GetTile());
        }
        else
        {
            KeyValuePair<Guid, SmartTileBase> tileAtPosition = this.SeedingTiles.GetTileAtPosition(clickedPosition);
            if (tileAtPosition.Key != Guid.Empty && tileAtPosition.Value != null)
            {
                Debug.Log($"Removing Tile at Position... {clickedPosition}");
                SeedingTiles.TileCollection.Remove(tileAtPosition.Key);
                TileMaps[TileMapType.TILLED.ToInt()].SetTile(clickedPosition, null);
            }
        }

        this.ManageCanvasMainTile(clickedPosition);


        KeyValuePair<Guid, SmartTileBase> keyValuePair = this._farmGrassTiles.GetTileAtPosition(clickedPosition);
        if (keyValuePair.Key != Guid.Empty && keyValuePair.Value != null)
        {
            Debug.Log($"Found Tile at Position {clickedPosition} - Id - {keyValuePair.Key}");
            Debug.Log(keyValuePair.Value.GetTile().name);
        }
    }

    private void ManageCanvasMainTile(Vector3Int gridPos)
    {
        this.SeedTileImage = this.SeedTileImage.GetComponent<Image>();
        this.MainTileImage = this.MainTileImage.GetComponent<Image>();

        KeyValuePair<Guid, SmartTileBase> tileAtPosition = SeedingTiles.GetTileAtPosition(gridPos);
        if(tileAtPosition.Key != Guid.Empty && tileAtPosition.Value != null)
        {
            Tile tile = tileAtPosition.Value.GetTile();
            if(tile.sprite != null)
                this.SeedTileImage.sprite = tile.sprite;
        }
        else
        {
            this.SeedTileImage.sprite = this.MainTileImage.sprite;
        }
    }

    public void ManageSelectedTileAtPosition(Vector3Int gridPos)
    {
        if(this._currentPos != gridPos)
        {
            TileMaps[TileMapType.SELECTOR.ToInt()].SetTile(this._currentPos, null);

            this.ManageCanvasMainTile(gridPos);

            this._currentPos = gridPos;
        }

        TileMaps[TileMapType.SELECTOR.ToInt()].SetTile(gridPos, Tiles[TileType.SELECTOR.ToInt()]);
    }
    

    private void MenuProcessing() { }

    public void OnMarketClick()
    {
        if(this.MarketCanvas.enabled)
        {
            this.MarketCanvas.GetComponent<Canvas>().enabled = false;
            Debug.Log("Leaving the Market!");
        }
        else
        {
            this.MarketCanvas.GetComponent<Canvas>().enabled = true;
            if(this.SeedsAndPlantsCanvas.enabled)
                this.SeedsAndPlantsCanvas.GetComponent<Canvas>().enabled = false;
            Debug.Log("Welcome to the Market!");
        }
    }

    public void OnPlantsAndSeedsClick()
    {
        // Image image = this.MarketCanvas.GetComponent<Image>();

        if(this.SeedsAndPlantsCanvas.enabled)
        {
            this.SeedsAndPlantsCanvas.GetComponent<Canvas>().enabled = false;
            //image.enabled = false;
            Debug.Log("Leaving the Seeds and Plants!");
        }
        else
        {
            this.SeedsAndPlantsCanvas.GetComponent<Canvas>().enabled = true;
            if(this.MarketCanvas.enabled)
                this.MarketCanvas.GetComponent<Canvas>().enabled = false;
            //image.enabled = true;
            Debug.Log("Welcome to the Seeds and Plants!");
        }
        //Debug.Log("Plants and Seeds UI.");
    }

    public void SeedButtonClick()
    {
        Debug.Log("Seed Click");
    }
    //TODO:: Make async
    public void CheckTilesDaily()
    {

        if(this.MarketCanvas.enabled || this.SeedsAndPlantsCanvas.enabled)
            return;

        if(completedDay == true)
        {

            completedDay = false;

            //foreach(KeyValuePair<Guid, SmartTileBase> farmTiles in this.SeedingTiles.TileCollection)
            //{
            //    Debug.Log($"Checking Seedling Tile! - {farmTiles.Key}");
               
            //    //switch(farmTiles.Value)
            //    //{
            //    //    case GrassTile gT:
            //    //        break;
            //    //    case BerryTile bT:
            //    //        break;
            //    //    case FruitTile fT:
            //    //        Debug.Log($"Fruit Tile Found! - {farmTiles.Value.GetPosition()}");
            //    //        break;
            //    //    case VeggieTile vT:
            //    //        break;
            //    //    default:
            //    //        break;
            //    //}

            //}

            completedDay = true;
            DayCount++;

            this.IncrementDay();


        }
        else
            return;
    }


    public void SpendActionButtonClick()
    {
        Debug.Log("Button clicked.");
        StartCoroutine("DumpActionQueue");
    }

    IEnumerator DumpActionQueue()
    {
        while (ActionQueue.ActionQueueManager.Count > 0)
        {
            ActionBase action = ActionQueue.ActionQueueManager.DequeueAction();
            TileMaps[TileMapType.TILLED.ToInt()].SetTile(action.GetPosition(), null);
            this.Player1.FarmingActionTaken(action);
            
            //GrassTile newTile = SmartTileFactory.Create<GrassTile>(CurrentMousePostion, Tiles[TileType.GRASS.ToInt()]);
            //TileMaps[TileMapType.GRASS.ToInt()].SetTile(action.GetPosition(), null);

            
            yield return new WaitForSeconds(1);
        }
    }
    */
}