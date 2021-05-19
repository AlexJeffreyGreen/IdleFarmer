using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Plants;
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
        this.Seed = s;
        this.SelectedSeedSprite.gameObject.SetActive(true);
        this.SelectedSeedSprite.sprite = s.GetSprites().Last();
        this.TextFields[0].text = this.Seed.Name;
        this.TextFields[1].text = $"{Seed.Price_Per_Seed}";
        this.TextFields[2].text = $"{Seed.Gestation_Period}";
        this.TextFields[3].text = $"{Seed.Price_At_Harvest}";
        this.TextFields[4].text = "0";
        foreach (Button interactableButton in this.InteractableButtons)
        {
            interactableButton.enabled = true;
        }
    }

    public void AddSeedClick()
    {
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
        amountToPurchaseText = 0;
        this.TextFields[4].text = amountToPurchaseText.ToString();
        Debug.Log("PurchaseSeedClick");
    }
}
