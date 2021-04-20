using Assets.Scripts.Farmer;
using Assets.Scripts.Farmer.Backpack;
using System.Collections;
using System.Collections.Generic;
using Farmer.Action;
using Farmer.Action.Farming;
using Farmer.Action.Stamina;
using UnityEngine;

public class FarmerPlayer : MonoBehaviour
{
    private BackpackBase _backpack;
    public BackpackBase Backpack { get { return this._backpack; } }

    public int MaxStamina = 100;
    public int CurrentStamina;
    public StaminaBar StaminaBar;

    // Start is called before the first frame update
    void Awake()
    {
        if(this._backpack == null)
            this._backpack = BackpackFactory.Create<SmallBackpack>();

        CurrentStamina = MaxStamina;
        StaminaBar.SetMaxStamina(MaxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.A)))
            this.FarmingActionTaken(ActionFactory.Create<FarmTile>());
            //this.UpdateStamina(-20);
        else if (Input.GetKeyDown(KeyCode.S))
            this.FarmingActionTaken(ActionFactory.Create<ReplenishStamina>());
            //this.UpdateStamina(20);
        
    }

    void UpdateStamina(int stamina)
    {
        //if(CurrentStamina)
        CurrentStamina += stamina;
        StaminaBar.SetStamina((CurrentStamina));
    }

    public void FarmingActionTaken(ActionBase action)
    {
        Debug.Log($"Action Offset = {action.GetActionStaminaOffSet()}");
        this.UpdateStamina(action.GetActionStaminaOffSet());
    }
}
