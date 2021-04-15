using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingHand : MonoBehaviour
{
    public List<FarmingCard> CurrentHand;
    public FarmingCard FarmingCardGeneric;
    public int MaxHand;
    public Vector3 HandPosition;
    // Start is called before the first frame update
    void Start()
    {
        if(CurrentHand == null || CurrentHand.Count == 0)
            PopulateHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PopulateHand()
    {
        // Vector3 previousPosition = HandPosition;

        int ACC = -MaxHand/2;
        for(int i = 0; i < MaxHand; i++)
        {
            FarmingCard card = Instantiate<FarmingCard>(FarmingCardGeneric);
            card.transform.SetParent(this.gameObject.transform);
            card.GetComponent<Transform>().localPosition = new Vector3(ACC, HandPosition.y);
            CurrentHand.Add(card);
            ACC += 2;
            //Really simple POC code... needs changing.

            //if(ACC == 0)
            //    ACC += 2;
            //else if(ACC > 0)
            //    ACC = -ACC;
            //else if(ACC < 0)
            //    ACC = +ACC;
        }
    }
}
