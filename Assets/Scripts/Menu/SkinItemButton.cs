using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinItemButton : MonoBehaviour
{
    public Image icon;

    private SkinItem item;


    public void Init(SkinItem item)
    {
        this.item = item;
        UpdateView();
    }

    public void EquipSkin()
    {
        GameData.instance.EquipItem(item.itemId, (int)item.category);
    }
    public void UnEquipSkin()
    {
        GameData.instance.EquipItem(item.itemId, (int)item.category);
    }

    private void UpdateView()
    {
        icon.sprite = item.itemIcon;
    }

}
