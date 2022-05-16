using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory", order = 0)]
public class InventoryObject : ScriptableObject
{
    public int inventorySize;
    public InventorySlot[] Container; // Private?
    //public List<InventorySlot> Container;

    private void Awake()
    {
        Container = new InventorySlot[inventorySize];
        //new List<InventorySlot>(inventorySize);
        InitializeSlots();
    }

    void InitializeSlots()
    {
        // Inýtialize empty slots later change here to get items from database
        for (int i = 0; i < inventorySize; i++)
        {
            Container[i] = new InventorySlot();
        }
    }

    public void AddItem(ItemObject item, ref int amounts)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (Container[i].Item == item && amounts > 0)
            {
                Container[i].TryAddAmounts(ref amounts);
            }
        }
        if (amounts > 0)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                if (!Container[i].Item)
                {
                    Container[i].TryAddItems(item, ref amounts);
                    if (amounts == 0) break;
                }
            }
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    //[SerializeField] private ItemObject _item;

    public ItemObject Item; // change to get private set later
    public int CurrentAmounts;
    public bool IsEmpty => CurrentAmounts == Item.maxStack;

    /// <summary>
    /// Use this to create inventory slot that contains amount of <paramref name="item"/>
    /// If <paramref name="amounts"/> is greater than maxStack of <paramref name="item"/>, 
    /// <paramref name="amounts"/> won't be zero  which means there will be remaining items 
    /// that couldn't be added to this inventory slot
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amounts"></param>
    public InventorySlot(ItemObject item, ref int amounts)
    {
        if(item)
        {
            Item = item;
            if (item.maxStack > amounts)
            {
                CurrentAmounts = amounts;
                amounts = 0;
            }
            else
            {
                CurrentAmounts = item.maxStack;
                amounts -= item.maxStack;
            }
        }
    }

    /// <summary>
    /// Use this to create empty inventory slot
    /// </summary>
    public InventorySlot()
    {
        Item = null;
        CurrentAmounts = 0;
    }

    /// <summary>
    /// Use this function to add <paramref name="amounts"/> of item to inventory slot
    /// </summary>
    /// <param name="amounts"></param>
    public void TryAddAmounts(ref int amounts)
    {
        if (Item)
        {
            CurrentAmounts += amounts;
            if (CurrentAmounts >= Item.maxStack)
            {
                amounts = CurrentAmounts - Item.maxStack;
                CurrentAmounts = Item.maxStack;
            } 
            else
            {
                amounts = 0;
            }
        }
    }

    public void TryAddItems(ItemObject item, ref int amounts)
    {
        if (!Item && item)
        {
            Item = item;
            CurrentAmounts = 0;
            //TryAddAmounts(ref amounts);
        }
        if (Item == item)
        {
            TryAddAmounts(ref amounts);
        }
        else
        {
            Debug.Log("There is a different item in this slot");
        }
    }
}
