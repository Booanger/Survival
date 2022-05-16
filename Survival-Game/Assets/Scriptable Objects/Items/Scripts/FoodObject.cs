using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food", order = 0)]
public class FoodObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Food;
    }
}
