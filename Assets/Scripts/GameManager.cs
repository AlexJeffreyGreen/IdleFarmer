//using Assets.Scripts;
//using Assets.Scripts;
using Assets.Scripts;
using Assets.Scripts.Plants;
using Assets.Scripts.SeedManager;
//using Assets.Scripts.SmartTiles;
//using Assets.Scripts.Tiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.Utilities.TileManagement;
using Farmer.Action;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager instance = null;
    //public Dictionary<Vector3Int, TileBase> SelectedTiles = new Dictionary<Vector3Int, TileBase>();
    #endregion
    
    #region Unity Inspector Properties

    [Header("Camera Properties")] 
    private Camera _mainCamera;
    [SerializeField] private float _mainCameraOffset_X;
    [SerializeField] private float _mainCameraOffset_Y;
    
    
    #endregion
    
    
    
    #region Unity Functions
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
     
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            this.SaveAndExit();
        }
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
}