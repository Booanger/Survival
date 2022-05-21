using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] InventoryObject _inventory;
    [SerializeField] private InventoryUI inventoryUI;

    public InventoryObject Inventory { get { return _inventory; } }
    

    public void AddItem(ItemObject item, ref int amount)
    {
        Inventory.AddItem(item, ref amount);
        //inventoryUI.UpdateSlots();
    }

    public void UpdateUI()
    {
        inventoryUI.UpdateSlots();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.UpdateSlots();
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        }
    }

    private void Start()
    {
        inventoryUI.gameObject.SetActive(false);
    }
}
