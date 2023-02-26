using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[System.Serializable]
public struct SkinData
{
    public int id;
    public GameObject gameObject;
}

public class SkinController : NetworkBehaviour
{
    //[Networked]
    int head = -1;
    int body = -1;
    int arms = -1;
    int overHead = -1;
    int overBody = -1;
    int overArms = -1;
    int voice = -1;

    public SkinData[] skinDataList;

    private void Start()
    {
        GameData.instance.localSkinContainer = this;
    }

    public void EquipSkin(int id, int category)
    {
        foreach (var item in skinDataList)
        {
            if (item.id == id)
            {
                item.gameObject.SetActive(true);
                if (item.gameObject.GetComponent<SkinBheaviour>() != null)
                {
                    item.gameObject.GetComponent<SkinBheaviour>().Equip();
                }
            }
        }
    }
    public void UnEquipSkin(int id, int category)
    {
        foreach (var item in skinDataList)
        {
            if (item.id == id)
            {
                item.gameObject.SetActive(false);
                if (item.gameObject.GetComponent<SkinBheaviour>() != null)
                {
                    item.gameObject.GetComponent<SkinBheaviour>().UnEquip();
                }
            }
        }
    }
}
