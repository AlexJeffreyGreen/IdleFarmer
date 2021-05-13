using UnityEngine;
using System;
using Assets.Scripts.Plants;
using Assets.Scripts.SeedManager;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.TileManagement;
using SaveSystem;
using UnityEngine.Tilemaps;


public class Loader : MonoBehaviour
{
    //public Camera mainCamera;
    public GameObject gameManager;
    public GameObject mapManager;
    public GameObject seedManager;
    public GameObject farmPlayer;
    public GameObject saveManager;
    public GameObject inventory;

    [SerializeField] private Texture2D _cursorSprite;
    //public SeedCollection SeedCollection;
    private void Awake()
    {
        if (SeedManager.instance == null)
            Instantiate(seedManager);
        if (GameManager.instance == null)
            Instantiate(gameManager);
        if (TileMapManager.instance == null)
            Instantiate(mapManager);
        if (FarmerPlayer.instance == null)
            Instantiate(farmPlayer);
        if (SaveManager.instance == null)
            Instantiate(saveManager);
        if (Inventory.instance == null)
            Instantiate(inventory);
        
        Cursor.SetCursor(_cursorSprite, Vector2.zero, CursorMode.Auto);
        
    }
    
    
}