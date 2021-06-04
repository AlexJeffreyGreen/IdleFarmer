using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Farmer.Backpack;
using Assets.Scripts.Plants;
using Assets.Scripts.SeedManager;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.UI;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField] private InventoryItem _InventoryItemPrefab;
    public List<InventoryItem> Items;
    [SerializeField] private GridLayoutGroup _layoutGroup;
    [SerializeField] private InventoryItemScriptable _inventoryItemTemplate;

    public BackpackType BackpackType;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        Items = new List<InventoryItem>();
        this.GenerateBackpack();
    }

    private void GenerateBackpack()
    {
        for (int i = 0; i < BackpackType.SlotCapacity; i++)
        {
            InventoryItem item = Instantiate(_InventoryItemPrefab);
            item.transform.SetParent(this._layoutGroup.transform);
            
            Items.Add(item);
            
        }
    }

    public void RemoveInventoryItem(Seed seed, int quantity)
    {
        
        InventoryItem item = this.Items.LastOrDefault(x =>x._InventoryItemScriptable != null  
                                                          && x._InventoryItemScriptable.Seed != null 
                                                          && x._InventoryItemScriptable.Seed == seed);
        if (item == null)
        {
            Debug.Log("No more items to remove of seed type.");
            return;
        }

        if (item._InventoryItemScriptable.Quantity <= item._InventoryItemScriptable.Capacity)
        {
            item._InventoryItemScriptable.Quantity -= quantity;
        }
        item.ReinitSeedSlot();
        //if (item._InventoryItemScriptable.Quantity <= 0)
        //{
    
            //this.Items.Remove(item);
        //}


    }

    public void AddInventoryItem(Seed seed, int quantity)
    {
        // if (!this.Items.Select(x => x._InventoryItemScriptable.Seed.SeedType).Contains(seedType))
        // {
        //     if (this.Items.Count < this.BackpackType.SlotCapacity)
        //     {
        //         
        //     }
        // }

        InventoryItem item = null;

        item = this.Items.FirstOrDefault(x =>x._InventoryItemScriptable != null  
                                             && x._InventoryItemScriptable.Seed != null 
                                             && x._InventoryItemScriptable.Seed == seed
                                             && x._InventoryItemScriptable.Capacity > x._InventoryItemScriptable.Quantity);


        if (item != null && item._InventoryItemScriptable.Capacity > item._InventoryItemScriptable.Quantity)
        {
            item._InventoryItemScriptable.Quantity += quantity;
            if (item._InventoryItemScriptable.Quantity > item._InventoryItemScriptable.Capacity)
            {
                int leftOvers = item._InventoryItemScriptable.Capacity - item._InventoryItemScriptable.Quantity;
                item._InventoryItemScriptable.Quantity = item._InventoryItemScriptable.Capacity;
                item.ReinitSeedSlot();
                if(leftOvers > 0)
                    this.AddInventoryItem(seed, leftOvers);
            }
            item.ReinitSeedSlot();
        }
        else
        {
            item = this.Items.FirstOrDefault(x => x._InventoryItemScriptable == null);
            if (item == null)
            {
                Debug.Log("No more inventory slots to add this seed.");
                return;
            }

            item._InventoryItemScriptable = ScriptableObject.Instantiate(this._inventoryItemTemplate);
            item._InventoryItemScriptable.Seed =
                SeedManager.instance.SeedCollection.Seed.First(x => x == seed);
            item._InventoryItemScriptable.Quantity = quantity;
            item._InventoryItemScriptable.Capacity = this.BackpackType.SeedCapacity;
            item.ReinitSeedSlot();
        }
    }

    private void ReorderIventoryItems()
    {
        //TODO - Decide what to do here.
    }

    /*//public BackpackUI BackpackUI;
    public List<InventoryItem> Items;

    [SerializeField] private InventoryItem itemPrefab;

    //[SerializeField] private RectTransform _rectTransform;
    
    [SerializeField] private GridLayoutGroup _layoutGroup;
    //public ScriptableObject InventoryItemUIElement;
    
    public void InitInventory()
    {
        Items = new List<InventoryItem>();
        for (int i = 0; i < FarmerPlayer.instance.Backpack.RetrieveIventoryItems().Count; i++)
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
        //TODO - Fix this garbage
        IList<List<Seed>> groups = FarmerPlayer.instance.Backpack.RetrieveIventoryItems().Select(x=>x.ItemSeed).GroupBy(x => x.SeedType).Select(y=>y.ToList()).ToList();

        foreach (List<Seed> seedGroupList in groups)
        {
            if (!this.Items.Select(x => x.ItemSeed).Contains(seedGroupList.First()))
            {
                InventoryItem i = Items.First(x=>x.ItemSeed == null);
                i.Quantity = seedGroupList.Count;
                i.ItemSeed = seedGroupList.First();
                i.UpsertSpriteAndQuantity(i.ItemSeed.SpriteCollection.SeedSprites.Last().GetSprite(), seedGroupList.Count);
            }
            else
            {
                Items.First(x => x.ItemSeed == seedGroupList.First()).UpdateQuanity(seedGroupList.Count);
            }
        }

      
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            UpdateInventory();
    }
    
    void Activate()
    {
        gameObject.SetActive(true);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }*/
}
