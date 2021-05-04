using UnityEngine;
using System;
using Assets.Scripts.Plants;
using Assets.Scripts.SeedManager;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.TileManagement;
using UnityEngine.Tilemaps;


public class Loader : MonoBehaviour
{
    //public Camera mainCamera;
    public GameObject gameManager;
    public GameObject mapManager;
    public GameObject seedManager;
    public GameObject farmPlayer;
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
    }
    
    
}