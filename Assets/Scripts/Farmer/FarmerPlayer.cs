using Assets.Scripts.Farmer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerPlayer : MonoBehaviour
{
    private Backpack _backpack;
    public Backpack Backpack { get { return this._backpack; } }


    // Start is called before the first frame update
    void Start()
    {
        if(this._backpack == null)
            this._backpack = new Backpack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
