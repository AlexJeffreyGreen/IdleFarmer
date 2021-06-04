using System;
using System.Linq;
using System.Runtime.Versioning;
using Assets.Scripts.Plants;
using Assets.Scripts.Utilities.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Farmer.Backpack
{
    public class InventoryItem : MonoBehaviour
    {

        public InventoryItemScriptable _InventoryItemScriptable;

        [SerializeField] private Text SpriteText;

        [SerializeField] private Button MainButton;
        private void Awake()
        {
            this.ClearData();
        }

        private void ClearData()
        {
            this.SpriteText.text = "";
            this.MainButton.enabled = false;
            this.MainButton.gameObject.GetComponent<Image>().enabled = false;
            this.MainButton.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               // this.ToggleUIElement();
            }
        }

        private void ToggleUIElement()
        {
            bool enabled = this.MainButton.enabled;
            this.SpriteText.text = (!enabled).ToString();
            this.MainButton.enabled = !enabled;
            this.MainButton.gameObject.GetComponent<Image>().enabled = !enabled;
            this.MainButton.gameObject.SetActive(!enabled);
        }

        public void ReinitSeedSlot()
        {
            if (this._InventoryItemScriptable.Quantity <= 0)
            {
                this._InventoryItemScriptable = null;
                this.SpriteText.text = "";
                this.MainButton.enabled = false;
                this.MainButton.gameObject.GetComponent<Image>().enabled = false;
                this.MainButton.gameObject.GetComponent<Image>().sprite = null;
                this.MainButton.gameObject.SetActive(false);
            }
            else
            {
                this.SpriteText.text = this._InventoryItemScriptable.Quantity.ToString();
                this.MainButton.enabled = true;
                this.MainButton.gameObject.GetComponent<Image>().enabled = true;
                this.MainButton.gameObject.GetComponent<Image>().sprite = this._InventoryItemScriptable.Seed
                    .GetSprites().Last();
                this.MainButton.gameObject.SetActive(true);
            }
        }

        public void SeedButtonClick()
        {
            Debug.Log($"Selected seed from inventory. {this._InventoryItemScriptable.Seed.Name}");
            FarmerPlayer.instance.UpdateSelectedSeed(this._InventoryItemScriptable.Seed);
        }

        //public void AddSeed(Seed seed, int Quantity)
        

        /*
        public void UpsertSpriteAndQuantity(Sprite s, int quantity = 0)
        {
            if (s == null)
            {
                this.SpriteText.text = "";
                this.ItemSeed = null;
                
                //this.MainButton.image.sprite = null;
                this.MainButton.gameObject.SetActive(false);
            }
            else
            {
                this.MainButton.gameObject.SetActive(true);
                this.MainButton.image.sprite = s;
                this.MainButton.enabled = true;
                this.SpriteText.text = $"{quantity}";
            }
        }

        public void UpdateQuanity(int quantity)
        {
            this.Quantity = quantity;
            if (quantity == 0)
            {
                this.UpsertSpriteAndQuantity(null);
            }
            else
                this.SpriteText.text = $"{quantity}";
        }

        private void Update()
        {
            //throw new NotImplementedException();
        }

        public void SelectSeed()
        {
            Debug.Log($"Selected seed! - {this.ItemSeed.Name}");
            if(this.ItemSeed != null)
                FarmerPlayer.instance.UpdateSelectedSeed(this.ItemSeed);

            Inventory m = this.GetComponentInParent<Inventory>();
            if (m != null)
            {
                BackpackUI bUi = m.GetComponentInParent<BackpackUI>();
                if(bUi != null)
                    bUi.OpenInventorySystemUI();
            }
        }
        */
        
        
    }
}