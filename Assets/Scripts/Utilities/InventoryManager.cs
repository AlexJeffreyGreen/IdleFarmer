using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Farmer.Backpack;
using Assets.Scripts.Plants;
using Assets.Scripts.Utilities;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int x_spacer;

    public int y_spacer;

    public int numberOfColumns;

    public List<InventoryItem> Items;

    [SerializeField] private InventoryItem itemPrefab;

    [SerializeField] private RectTransform _rectTransform;
    
    [SerializeField] private GridLayoutGroup _layoutGroup;
    //public ScriptableObject InventoryItemUIElement;
    
    // Start is called before the first frame update
    void Start()
    {
        Items = new List<InventoryItem>();
        for (int i = 0; i < FarmerPlayer.instance.Backpack.MaxSeeds(); i++)
        {
            InventoryItem item = Instantiate(itemPrefab, new Vector3(0, 0, 10), Quaternion.identity);
            item.Quantity = 0;
            item.ItemSeed = null;
            item.UpdateQuanity(0);
            item.transform.SetParent(this._layoutGroup.transform);
            Items.Add(item);
        }
    }

    public void UpdateInventory()
    {
        IList<List<Seed>> groups = FarmerPlayer.instance.Backpack.RetrieveSeeds().GroupBy(x => x.SeedType).Select(y=>y.ToList()).ToList();
        

        
        foreach (List<Seed> seedGroupList in groups)
        {
            if (!this.Items.Select(x => x.ItemSeed).Contains(seedGroupList.First()))
            {
                InventoryItem i = Items.First(x=>x.ItemSeed == null);
                i.Quantity = seedGroupList.Count;
                i.ItemSeed = seedGroupList.First();
                i.InitSpriteAndQuantity(i.ItemSeed.SpriteCollection.SeedSprites.Last().GetSprite(), seedGroupList.Count);
                // InventoryItem item = Instantiate(itemPrefab, new Vector3(0, 0, 10), Quaternion.identity);
                // item.Quantity = seedGroupList.Count;
                // item.ItemSeed = seedGroupList.First();
                // item.transform.SetParent(this._layoutGroup.transform);
                // //item.transform. = this._rectTransform.transform.localPosition;
                // item.InitSpriteAndQuantity(item.ItemSeed.SpriteCollection.SeedSprites.Last().GetSprite(),
                //     seedGroupList.Count);
                // this.Items.Add(item);
            }
            else
            {
                // InventoryItem i = Items.First(x => x.ItemSeed == seedGroupList.First());
                // i.Quantity = seedGroupList.Count;
                // Items.Remove(i);
                // Items.Add(i);
                Items.First(x => x.ItemSeed == seedGroupList.First()).UpdateQuanity(seedGroupList.Count);
            }
            //item.InitSprite();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            UpdateInventory();
    }
}
