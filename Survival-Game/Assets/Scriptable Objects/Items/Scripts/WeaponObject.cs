using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon", order = 0)]
public class WeaponObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Weapon;
    }
}
