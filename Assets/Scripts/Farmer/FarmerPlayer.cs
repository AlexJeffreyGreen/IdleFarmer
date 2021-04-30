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

    //public GameManager GameManager;
    private Vector3Int previousPosition;
    
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
        /*if (Input.GetMouseButtonDown(0) && GameManager.instance.IsMouseOverTile())
        {
            Vector3Int currentPos = GameManager.instance.GetCurrentPosition();

            if (!ActionQueue.ActionQueueManager.AnyAtPosition(currentPos))
            {
                ActionQueue.ActionQueueManager.EnqueueAction(ActionFactory.Create<FarmTile>(currentPos));
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ActionQueue.ActionQueueManager.EnqueueAction(ActionFactory.Create<ReplenishStamina>());
        }*/
    }

    void UpdateStamina(int stamina)
    {
        //if(CurrentStamina)
        CurrentStamina += stamina;
        StaminaBar.SetStamina((CurrentStamina));
    }

    public void FarmingActionTaken(ActionBase action)
    {
        //Debug.Log($"Action Offset = {action.GetActionStaminaOffSet()}");
        //Debug.Log($"Action at Position {action.GetPosition()}");
        //this.UpdateStamina(action.GetActionStaminaOffSet());
        //if(action is FarmTile)
        //    GameManager.HandleTileClick(action.GetPosition());
    }
}
