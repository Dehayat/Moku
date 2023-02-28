using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Moku/ShopItem")]
public class ShopItem : ScriptableObject
{
    public ItemData itemData;
    public float itemCost;

    public int itemId
    {
        get
        {
            return itemData.itemId;
        }
    }
    public Sprite itemIcon
    {
        get
        {
            return itemData.itemIcon;
        }
    }
}
