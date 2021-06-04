using System;
using Assets.Scripts.Farmer;
using Assets.Scripts.Farmer.Backpack;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Plants;
using Assets.Scripts.SeedManager;
using Farmer.Action;
using UnityEngine;
using Random = System.Random;
using Assets.Scripts.Farmer.Action;

public class FarmerPlayer : MonoBehaviour
{
    public static FarmerPlayer instance = null;
    //private BackpackBase _backpack;
   // public BackpackBase Backpack { get { return this._backpack; } }
    private int testACC;

    private Seed SelectedSeed;

    public Wallet Wallet;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        if(Wallet == null)
            this.Wallet = new Wallet();
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
        {
            this.SelectedSeed = SeedManager.instance.SeedCollection.Seed[testACC];
            testACC++;
            if (testACC > SeedManager.instance.SeedCollection.Seed.Length - 1)
                testACC = 0; 
            Debug.Log("Farmer player pressed space.");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
           Inventory.instance.AddInventoryItem(this.SelectedSeed, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Inventory.instance.RemoveInventoryItem(this.SelectedSeed, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            this.Wallet.ModifyWallet(-100);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            this.Wallet.ModifyWallet(100);
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
