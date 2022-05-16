using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemObject item;
    public int amount; // Public?

    public void Interact(InteractionHandler interactor)
    {
        Debug.Log("Interacting with " + item.name);
        interactor.Inventory.inventory.AddItem(item, ref amount);
        if (amount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
