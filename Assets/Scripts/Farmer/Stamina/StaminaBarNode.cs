using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBarNode : MonoBehaviour
{
    public int MinStamina;
    public int MaxStamina;
    private int _currentStaminaPoints;
    // Start is called before the first frame update
    void Awake()
    {
        _currentStaminaPoints = MaxStamina;
    }

    public int CurrentStamina => this._currentStaminaPoints;

    public void UpdateStamina(int acc)
    {
        if(acc >= MaxStamina)
        {
            this._currentStaminaPoints = this.MaxStamina;
        }
        else if(this._currentStaminaPoints + acc <= MinStamina)
        {
            this._currentStaminaPoints = this.MinStamina;
        }
        else
            this._currentStaminaPoints += acc;
    }

   


}
