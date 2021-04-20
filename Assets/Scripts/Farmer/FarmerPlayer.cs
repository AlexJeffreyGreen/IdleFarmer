using Assets.Scripts.Farmer;
using Assets.Scripts.Farmer.Backpack;
using System.Collections;
using System.Collections.Generic;
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

        StaminaBar.SetMaxStamina((MaxStamina));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.Space)))
            this.StaminaTest(20);
    }

    void StaminaTest(int stamina)
    {
        //if(CurrentStamina)
        CurrentStamina -= stamina;
        StaminaBar.SetStamina((CurrentStamina));
    }
}
