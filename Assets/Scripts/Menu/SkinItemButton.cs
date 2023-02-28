using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinItemButton : MonoBehaviour
{
    public Image icon;

    private ItemData item;

    public void Init(ItemData item)
    {
        this.item = item;
        UpdateView();
    }

    public void ToggleEquip()
    {
        if (GameData.instance.isItemEquiped(item))
        {
            UnEquipSkin();
        }
        else
        {
            EquipSkin();
        }
    }

    public void EquipSkin()
    {
        GameData.instance.EquipItem(item);
    }
    public void UnEquipSkin()
    {
        GameData.instance.UnEquipItem(item);
    }

    private void UpdateView()
    {
        icon.sprite = item.itemIcon;
    }

}
