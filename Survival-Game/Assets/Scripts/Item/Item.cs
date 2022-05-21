using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemObject _item;
    public int amount; // Public?

    public void Interact(Interactor interactor)
    {
        interactor.Inventory.AddItem(_item, ref amount);
        interactor.Inventory.UpdateUI();
        Debug.Log("Interacting with " + _item.name);

        TryDestroy();
    }

    void TryDestroy()
    {
        if (amount < 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Inventory is full");
        }
    }

    public Item(ItemObject item, int amount)
    {
        _item = item;
        amount = amount;
    }
}
