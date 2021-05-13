using System;
using Assets.Scripts.Farmer;
using Assets.Scripts.Farmer.Backpack;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Plants;
using Assets.Scripts.SeedManager;
using Farmer.Action;
using Farmer.Action.Farming;
using Farmer.Action.Stamina;
using UnityEngine;
using Random = System.Random;

public class FarmerPlayer : MonoBehaviour
{
    public static FarmerPlayer instance = null;
    //private BackpackBase _backpack;
   // public BackpackBase Backpack { get { return this._backpack; } }
    private int testACC;

    private Seed SelectedSeed;
    

    void Awake()
    {
      //  if(this._backpack == null)
         //   this._backpack = BackpackFactory.Create<SmallBackpack>();

        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO - Only temporary
        TestingMethod();
    }

    private void TestingMethod()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            this.SelectedSeed = SeedManager.instance.SeedCollection.Seed.First();
        else if (Input.GetKeyDown(KeyCode.A))
        {
           Inventory.instance.AddInventoryItem(this.SelectedSeed.SeedType, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Inventory.instance.RemoveInventoryItem(this.SelectedSeed.SeedType, 1);
        }
    }


    public void UpdateSelectedSeed(Seed s)
    {
        if(s != null)
            this.SelectedSeed = s;
        //do something more here!
    }

    public Seed GetSelectedSeed()
    {
        return this.SelectedSeed;
    }

    // void UpdateStamina(int stamina)
    // {
    //     //if(CurrentStamina)
    //     CurrentStamina += stamina;
    //     StaminaBar.SetStamina((CurrentStamina));
    // }

    public void FarmingActionTaken(ActionBase action)
    {
        //Debug.Log($"Action Offset = {action.GetActionStaminaOffSet()}");
        //Debug.Log($"Action at Position {action.GetPosition()}");
        //this.UpdateStamina(action.GetActionStaminaOffSet());
        //if(action is FarmTile)
        //    GameManager.HandleTileClick(action.GetPosition());
    }
}
