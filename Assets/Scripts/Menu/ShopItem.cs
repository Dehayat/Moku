using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Moku/ShopItem")]
public class ShopItem : ScriptableObject
{
    public int itemId;
    public float itemCost;
    public Sprite itemIcon;
}
