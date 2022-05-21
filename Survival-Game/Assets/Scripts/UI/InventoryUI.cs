using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] Transform slotHolderGrid;
    [SerializeField] InventorySystem playerInventory;
    [SerializeField] GameObject slotPrefab;

    private Transform currentHoveredSlot;

    [Space]
    public List<Transform> slots;

    // Start is called before the first frame update
    void Start()
    {
        InitializeSlots();
        UpdateSlots();
    }

    private void InitializeSlots()
    {
        for (int i = 0; i < playerInventory.Inventory.Container.Length; i++)
        {
            Instantiate(slotPrefab, slotHolderGrid);
        }
        for (int i = 0; i < slotHolderGrid.childCount; i++)
        {
            Transform currentSlot = slotHolderGrid.GetChild(i);
            slots.Add(currentSlot);

            currentSlot.GetComponent<Button>().onClick.AddListener(OnClick);

            
            EventTrigger.Entry entry = new()
            {
                eventID = EventTriggerType.PointerEnter,
                callback = new EventTrigger.TriggerEvent()
            };
            entry.callback.AddListener((eventData) => 
            { 
                OnPointerEnterToButton(currentSlot); 
            });

            currentSlot.GetComponent<EventTrigger>().triggers.Add(entry);
        }
    }

    private void OnPointerEnterToButton(Transform currentSlot) => currentHoveredSlot = currentSlot;

    public void UpdateSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].TryGetComponent<Image>(out Image tempImage);
            if (tempImage)
            {
                if (playerInventory.Inventory.Container[i].CurrentAmounts > 0)
                {
                    tempImage.sprite = playerInventory.Inventory.Container[i].Item.sprite;
                }
                else
                {
                    tempImage.sprite = null;
                }
            }

            TextMeshProUGUI amount = slots[i].GetComponentInChildren<TextMeshProUGUI>();
            if (amount) amount.text = playerInventory.Inventory.Container[i].CurrentAmounts.ToString();
        }
    }

    void OnClick()
    {
        Transform test = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>();
        int index = slots.FindIndex(x => x == test);

        Debug.Log("Slot is Clicked! " + index);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //UI UPDATE
            currentHoveredSlot.TryGetComponent<Image>(out Image tempImage);
            tempImage.sprite = null;

            TextMeshProUGUI amount = currentHoveredSlot.GetComponentInChildren<TextMeshProUGUI>();
            amount.text = "0";

            //ITEM SPAWN
            int index = slots.FindIndex(x => x == currentHoveredSlot);
            GameObject itemPrefabToSpawn = playerInventory.Inventory.Container[index].Item.prefab;

            Debug.Log(playerInventory.Inventory.Container[index].CurrentAmounts);

            Item item = itemPrefabToSpawn.GetComponent<Item>();
            item.amount = playerInventory.Inventory.Container[index].CurrentAmounts;

            Instantiate(itemPrefabToSpawn, playerInventory.gameObject.transform.position + playerInventory.gameObject.transform.forward, Quaternion.identity);

            //REMOVE ITEM FROM INVENTORY
            playerInventory.Inventory.RemoveItem(index);

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The cursor is now over Inventory.");
    }

    /*
    public void OnPointerEnterToButton(PointerEventData data)
    {
        Debug.Log("Mouse Slot Üzerine Geldi");
        currentHoveredSlot = data.pointerCurrentRaycast.gameObject.transform;
        currentHoveredSlot.GetComponent<Image>().color = Color.red;
    }*/


}
