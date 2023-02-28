using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory
{
    head,
    arm,
    body,
    overHead,
    overBody,
    overArm,
    voice
}

[CreateAssetMenu(menuName = "Moku/Item")]
public class ItemData : ScriptableObject
{
    public int itemId;
    public Sprite itemIcon;
    public ItemCategory category;
}
