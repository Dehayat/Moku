using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSkinBehaviour : SkinBehaviour
{
    public GameObject[] skinParts;
    override public void Equip()
    {
        foreach (var part in skinParts)
        {
            part.SetActive(true);
        }
    }
    override public void UnEquip()
    {
        foreach (var part in skinParts)
        {
            part.SetActive(false);
        }
    }
}
