using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Plants;
using Assets.Scripts.Shop;
using UnityEngine;
using UnityEngine.UI;

public class HighlightedSeed : MonoBehaviour
{
    // Start is called before the first frame update
    //public Sprite HighlightedSeed_Sprite;
    private Seed Seed;

    //public Button Add;
    //public Button Subtract;
    //public Button PurchaseButton;
    [SerializeField] private Image SelectedSeedSprite;
    public List<Text> TextFields;
    public Button[] InteractableButtons;
    public MarketItem item;
    

    private int amountToPurchaseText;
    
    void Start()
    {
        this.Seed = null;
        this.SelectedSeedSprite.gameObject.SetActive(false);
        this.TextFields[0].text = "";
        this.TextFields[1].text = "";
        this.TextFields[2].text = "";
        this.TextFields[3].text = "";
        this.TextFields[4].text = "0";
        foreach (Button interactableButton in this.InteractableButtons)
        {
            interactableButton.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeHighlightedSeed(Seed s)
    {
        this.item = Market.instance.GetMarketItem(s);
        this.Seed = s;
        this.SelectedSeedSprite.gameObject.SetActive(true);
        this.SelectedSeedSprite.sprite = s.GetSprites().Last();
        this.TextFields[0].text = this.Seed.Name;
        this.TextFields[1].text = $"{Seed.Price_Per_Seed}";
        this.TextFields[2].text = $"{Seed.Gestation_Period}";
        this.TextFields[3].text = $"{Seed.Price_At_Harvest}";
        this.TextFields[4].text = "0";
        this.amountToPurchaseText = 0;
        foreach (Button interactableButton in this.InteractableButtons)
        {
            interactableButton.enabled = true;
        }
    }

    public void AddSeedClick()
    {
        if(item.Quantity < amountToPurchaseText)
        {
            Debug.Log("Cannot purchase anymore. Reached the max available of said seed in the market.");
            return;
        }
        amountToPurchaseText++;
        this.TextFields[4].text = amountToPurchaseText.ToString();
        Debug.Log("Add Click");
    }

    public void SubtractSeedClick()
    {
        amountToPurchaseText--;
        if (amountToPurchaseText <= 0)
            amountToPurchaseText = 0;
        this.TextFields[4].text = amountToPurchaseText.ToString();
        Debug.Log("Subtract Click");
    }

    public void PurchaseSeedClick()
    {
        Debug.Log($"Purchased: {amountToPurchaseText} Seeds");
        int amountToBePurchased = Convert.ToInt32(amountToPurchaseText);
        //Calculate total purchased.
        decimal cost = amountToBePurchased * Convert.ToDecimal(Seed.Price_Per_Seed);
        //Calculate if player has enough money
        decimal amountPlayerHasInWallet = FarmerPlayer.instance.Wallet.Amount;
        if(cost > amountPlayerHasInWallet)
        {
            Debug.Log("Far too much was requested to purchase.");
            return;
        }
        //if not, make noise.

        //if yes, revert values add to inventory.
        else
        {
            FarmerPlayer.instance.Wallet.ModifyWallet(cost);
            Inventory.instance.AddInventoryItem(Seed, amountToBePurchased);
            Market.instance.UpdateMarketItem(Seed, -amountToBePurchased);
        }

        amountToPurchaseText = 0;
        this.TextFields[4].text = amountToPurchaseText.ToString();



        Debug.Log("PurchaseSeedClick");
    }
}
