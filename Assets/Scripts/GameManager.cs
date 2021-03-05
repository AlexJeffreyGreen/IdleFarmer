//using Assets.Scripts;
//using Assets.Scripts;
using Assets.Scripts;
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

    //TILES AND MAPS
    public Grid grid;
    public Tilemap _farmMap;
    public Tile _farmTile;
    public Tilemap _selectorMap;
    public Tile _selectionTile;
    public Tilemap _plantMap;
    public Tile _hoedGrassTile;

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
            GrassTile newTile = SmartTileFactory.Create<GrassTile>(p, this._farmTile);

            this._farmGrassTiles.TileCollection.Add(Guid.NewGuid(),newTile);

            this._farmMap.SetTile(p, newTile.GetTile());
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
        Vector3Int gridPos = _farmMap.WorldToCell(mousePos);
        if(_farmMap.HasTile(gridPos))
        {
            if(this._currentPos != gridPos)
            {
                _selectorMap.SetTile(this._currentPos, null);
                this._currentPos = gridPos;
            }

            _selectorMap.SetTile(gridPos, _selectionTile);

            if(Input.GetMouseButtonDown(0))
            {
                if(!_plantMap.HasTile(gridPos))
                {
                    Debug.Log($"Adding Tile at Position... {gridPos}");

                    GrassTile tile = SmartTileFactory.Create<GrassTile>(gridPos, this._hoedGrassTile);
                    this.SeedingTiles.TileCollection.Add(Guid.NewGuid(), tile);
                    _plantMap.SetTile(gridPos, tile.GetTile());
                }
                else
                {
                    KeyValuePair<Guid, SmartTileBase> tileAtPosition = this.SeedingTiles.GetTileAtPosition(gridPos);
                    if(tileAtPosition.Key != Guid.Empty && tileAtPosition.Value != null)
                    {
                        Debug.Log($"Removing Tile at Position... {gridPos}");
                        SeedingTiles.TileCollection.Remove(tileAtPosition.Key);
                        _plantMap.SetTile(gridPos, null);
                    }
                }


                KeyValuePair<Guid,SmartTileBase> keyValuePair = this._farmGrassTiles.GetTileAtPosition(gridPos);
                if(keyValuePair.Key != Guid.Empty && keyValuePair.Value != null)
                {
                    Debug.Log($"Found Tile at Position {gridPos} - Id - {keyValuePair.Key}");
                    Debug.Log(keyValuePair.Value.GetTile().name);
                }
            }
        }
        else
        {
            _selectorMap.ClearAllTiles();
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


    //TODO:: Make async
    public void CheckTilesDaily()
    {

        if(this.MarketCanvas.enabled || this.SeedsAndPlantsCanvas.enabled)
            return;

        if(completedDay == true)
        {
              
                 completedDay = false;

                 foreach(KeyValuePair<Guid, SmartTileBase> farmTiles in this.SeedingTiles.TileCollection)
                     Debug.Log($"Checking Seedling Tile! - {farmTiles.Key}");

                 completedDay = true;
                 DayCount++;

                 this.IncrementDay();
             
             
        }
        else
            return;
    }
}