using System;
using System.Linq;
using Assets.Scripts.Plants;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Utilities
{
    public class InventoryItem : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
    {
        public Color MainColor;
        public Seed ItemSeed;
        public int Quantity;

        [SerializeField] private Image SeedSpriteContainer;
        [SerializeField] private Text SpriteText;

        [SerializeField]
        private Image ContainerImage;
        private void Awake()
        {
            SpriteText.text = "";
            SeedSpriteContainer.enabled = false;
            ContainerImage = this.GetComponent<Image>();
        }

        public void InitSpriteAndQuantity(Sprite s, int quantity)
        {
            this.SeedSpriteContainer.sprite = s;
            this.SeedSpriteContainer.enabled = true;
            this.SpriteText.text = $"{quantity}";
        }

        public void UpdateQuanity(int quantity)
        {
            this.Quantity = quantity;
            if (quantity == 0)
                this.SpriteText.text = "";
            else
                this.SpriteText.text = $"{quantity}";
        }

        /*
        private void OnMouseOver()
        {
            Debug.Log("Mouse over");
        }

        private void OnMouseEnter()
        {
            Debug.Log("Mouse enter");
            //throw new NotImplementedException();
        }

        private void OnMouseExit()
        {
            Debug.Log("Mouse exit");
            //throw new NotImplementedException();
        }
        */

        private void Update()
        {
            //throw new NotImplementedException();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            this.MainColor = this.ContainerImage.material.color;
            this.ContainerImage.color = Color.grey;
            Debug.Log("Pointer entered.");
          // throw new NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            this.ContainerImage.color = this.MainColor;
            Debug.Log("Pointer exited.");
           // throw new NotImplementedException();
        }
    }
}