using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkinCategory
{
    head,
    arm,
    body,
    overHead,
    overBody,
    overArm,
    voice
}

[CreateAssetMenu(menuName = "Moku/SkinItem")]
public class SkinItem : ScriptableObject
{
    public int itemId;
    public Sprite itemIcon;
    public SkinCategory category;
}
