using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory", order = 0)]
public class InventoryObject : ScriptableObject
{
    public int Size => Container.Length;
    public InventorySlot[] Container; // Private?
    //public List<InventorySlot> Container;

    private void Awake()
    {
        Container = new InventorySlot[Size];
        //new List<InventorySlot>(inventorySize);
        InitializeSlots();

        
    }

    void InitializeSlots()
    {
        // Inýtialize empty slots later change here to get items from database
        for (int i = 0; i < Size; i++)
        {
            Container[i] = new InventorySlot();
        }
    }

    public void AddItem(ItemObject item, ref int amounts)
    {
        for (int i = 0; i < Size; i++)
        {
            if (Container[i].Item == item && amounts > 0)
            {
                Container[i].TryAddAmounts(ref amounts);
            }
        }
        if (amounts > 0)
        {
            for (int i = 0; i < Size; i++)
            {
                if (!Container[i].Item)
                {
                    Container[i].TryAddItems(item, ref amounts);
                    if (amounts == 0) break;
                }
            }
        }
    }

    public void RemoveItem(int index)
    {
        Container[index].ClearSlot();
        //Container[index].Item = null;
        //Container[index].CurrentAmounts = 0;
    }
}

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private ItemObject _item;
    [SerializeField] private int _amount;

    public ItemObject Item 
    { 
        get { return _item; } 
        //set { _item = value; } 
    
    }

    public int CurrentAmounts { get { return _amount; } }

    public int index;

    public bool IsFull => CurrentAmounts == Item.maxStack;

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
            _item = item;
            if (item.maxStack > amounts)
            {
                _amount = amounts;
                amounts = 0;
            }
            else
            {
                _amount = item.maxStack;
                amounts -= item.maxStack;
            }
        }
    }

    /// <summary>
    /// Use this to create empty inventory slot
    /// </summary>
    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        _item = null;
        _amount = 0;
    }

    /// <summary>
    /// Use this function to add <paramref name="amounts"/> of item to inventory slot
    /// </summary>
    /// <param name="amounts"></param>
    public void TryAddAmounts(ref int amounts)
    {
        if (Item)
        {
            _amount += amounts;
            if (CurrentAmounts >= Item.maxStack)
            {
                amounts = CurrentAmounts - Item.maxStack;
                _amount = Item.maxStack;
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
            _item = item;
            _amount = 0;
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
