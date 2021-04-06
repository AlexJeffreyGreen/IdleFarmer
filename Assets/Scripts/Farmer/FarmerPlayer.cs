using Assets.Scripts.Farmer;
using Assets.Scripts.Farmer.Backpack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerPlayer : MonoBehaviour
{
    private BackpackBase _backpack;
    public BackpackBase Backpack { get { return this._backpack; } }


    // Start is called before the first frame update
    void Awake()
    {
        if(this._backpack == null)
            this._backpack = BackpackFactory.Create<SmallBackpack>();



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
