//using Assets.Scripts;
//using Assets.Scripts;
using Assets.Scripts;
using Assets.Scripts.Plants;
using Assets.Scripts.Plants.Fruit;
using Assets.Scripts.Plants.Vegtable;
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

    //GRID SIZING
    public int xVar;
    public int yVar;
   
    //CURRENT GRID POSITION
    private Vector3Int _currentPos;
    public Vector3Int CurrentPosition { get { return this._currentPos; } }

    //EXTRAS
    public System.Random Random;
    public TileType TileType;

    private void Awake()
    {
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

        InvokeRepeating("CheckTilesDaily", 2.0f, 15.0f);
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

    private void TileMovement()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = TileMaps[TileMapType.GRASS.ToInt()].WorldToCell(mousePos);
        if(TileMaps[0].HasTile(gridPos))
        {
            if(this._currentPos != gridPos)
            {
                TileMaps[TileMapType.SELECTOR.ToInt()].SetTile(this._currentPos, null);
                this._currentPos = gridPos;
            }

            TileMaps[TileMapType.SELECTOR.ToInt()].SetTile(gridPos, Tiles[TileType.SELECTOR.ToInt()]);

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

                        //
                        // Basic Test / Proof of adding different tiles instead of the tilled soil tile.
                        //
                        FruitTile tile = SmartTileFactory.Create<FruitTile>(gridPos, Tiles[TileType.TILLED.ToInt()]);
                        Debug.Log($"Replacing Tile with Fruit Tile - Test");
                        SeedingTiles.TileCollection.Add(Guid.NewGuid(), tile);
                        TileMaps[TileMapType.TILLED.ToInt()].SetTile(gridPos, tile.GetTile());
                    }
                }


                KeyValuePair<Guid,SmartTileBase> keyValuePair = this._farmGrassTiles.GetTileAtPosition(gridPos);
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
        }
        else
        {
            TileMaps[TileMapType.SELECTOR.ToInt()].ClearAllTiles();
        }
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

            foreach(KeyValuePair<Guid, SmartTileBase> farmTiles in this.SeedingTiles.TileCollection)
            {
                Debug.Log($"Checking Seedling Tile! - {farmTiles.Key}");
               
                switch(farmTiles.Value)
                {
                    case GrassTile gT:
                        break;
                    case BerryTile bT:
                        break;
                    case FruitTile fT:
                        Debug.Log($"Fruit Tile Found! - {farmTiles.Value.GetPosition()}");
                        break;
                    case VeggieTile vT:
                        break;
                    default:
                        break;
                }

            }

            completedDay = true;
            DayCount++;

            this.IncrementDay();


        }
        else
            return;
    }
}