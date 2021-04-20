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
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
    public Vector3Int CurrentPosition { get { return this._currentPos; } }

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
            /*if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Pressed A");
                //StaminaTest(-20);
                //this.StaminaBarManager.RemoveStamina(-50);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Pressed S");
                //StaminaTest(20);
                //this.StaminaBarManager.AddStamina(50);
            }*/
        }
    }

 
        
    private void LateUpdate()
    {
        
    }

    private void TileMovement()
    {
        Vector3 tileLocation = Input.mousePosition;
        Ray screenWorldToRay = Camera.main.ScreenPointToRay(tileLocation);
        Vector3 worldPoint = screenWorldToRay.GetPoint(-screenWorldToRay.origin.z / screenWorldToRay.direction.z);
        worldPoint = new Vector3(worldPoint.x - CameraOffset, worldPoint.y - CameraOffset, worldPoint.z);
        Vector3Int gridPos = TileMaps[TileMapType.GRASS.ToInt()].WorldToCell(worldPoint);

        if(TileMaps[0].HasTile(gridPos))
        {

            ManageSelectedTileAtPosition(gridPos);


            if(Input.GetMouseButtonDown(0))
            {
                if(!TileMaps[TileMapType.TILLED.ToInt()].HasTile(gridPos))
                {
                    Debug.Log($"Adding Tile at Position... {gridPos}");

                    GrassTile tile = SmartTileFactory.Create<GrassTile>(gridPos, Tiles[TileType.TILLED.ToInt()]);
                    this.SeedingTiles.TileCollection.Add(Guid.NewGuid(), tile);
                    TileMaps[TileMapType.TILLED.ToInt()].SetTile(gridPos, tile.GetTile());
                }
                else
                {
                    KeyValuePair<Guid, SmartTileBase> tileAtPosition = this.SeedingTiles.GetTileAtPosition(gridPos);
                    if(tileAtPosition.Key != Guid.Empty && tileAtPosition.Value != null)
                    {
                        Debug.Log($"Removing Tile at Position... {gridPos}");
                        SeedingTiles.TileCollection.Remove(tileAtPosition.Key);
                        TileMaps[TileMapType.TILLED.ToInt()].SetTile(gridPos, null);
                    }
                }

                this.ManageCanvasMainTile(gridPos);


                KeyValuePair<Guid, SmartTileBase> keyValuePair = this._farmGrassTiles.GetTileAtPosition(gridPos);
                if(keyValuePair.Key != Guid.Empty && keyValuePair.Value != null)
                {
                    Debug.Log($"Found Tile at Position {gridPos} - Id - {keyValuePair.Key}");
                    Debug.Log(keyValuePair.Value.GetTile().name);
                }
            }
            else
            {
                //clear?
            }

            // ManageCanvasMainTile(gridPos);
        }
        else
        {
            //We should temp store the previous tile position so it looks less janky
            //TileMaps[TileMapType.SELECTOR.ToInt()].ClearAllTiles();
        }
    }

    private void ManageCanvasMainTile(Vector3Int gridPos)
    {
        //this.MainTileImage;
        this.SeedTileImage = this.SeedTileImage.GetComponent<Image>();
        this.MainTileImage = this.MainTileImage.GetComponent<Image>();
        //TileBase currentTile = TileMaps[TileMapType.GRASS.ToInt()].GetTile(gridPos);
        //if(this._currentPos != gridPos)
        //{
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
        //}
    }

    private void ManageSelectedTileAtPosition(Vector3Int gridPos)
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
}