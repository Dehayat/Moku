using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinBheaviour : MonoBehaviour
{
    public GameObject[] skinParts;
    public void Equip()
    {
        foreach (var part in skinParts)
        {
            part.SetActive(true);
        }
    }
    public void UnEquip()
    {
        foreach (var part in skinParts)
        {
            part.SetActive(false);
        }
    }
}
