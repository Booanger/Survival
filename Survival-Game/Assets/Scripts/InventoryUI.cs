using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] Transform slotHolderGrid;
    [SerializeField] Inventory playerInventory;
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
        for (int i = 0; i < playerInventory.inventory.Container.Length; i++)
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

            entry.callback.AddListener((eventData) => { OnPointerEnterToButton(currentSlot); });
            //entry.callback.AddListener(delegate { OnPointerEnterToButton(PointerEventData); });
            //UnityEngine.Events.UnityAction<BaseEventData> call = new UnityEngine.Events.UnityAction<BaseEventData>(OnPointerEnterToButton);
            currentSlot.GetComponent<EventTrigger>().triggers.Add(entry);
        }
    }

    private void OnPointerEnterToButton(Transform currentSlot)
    {
        currentHoveredSlot = currentSlot;
        currentHoveredSlot.GetComponent<Image>().color = Color.red;
    }

    private void UpdateSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Image temp;
            slots[i].TryGetComponent<Image>(out temp);
            if (temp) temp.sprite = playerInventory.inventory.Container[i].Item.sprite;  
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
