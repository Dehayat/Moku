using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using System;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    public ItemList itemList;
    public SkinController localSkinContainer;
    public ItemData[] defaultItems;

    private HashSet<int> items;

    private Dictionary<ItemCategory, ItemData> equipedItems;


    public ItemData GetItemById(int id)
    {
        return itemList.GetItemById(id);
    }


    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
    }

    public void SetLocalSkinController(SkinController local)
    {
        localSkinContainer = local;
        foreach (var item in equipedItems)
        {
            local.EquipSkin(item.Value);
        }
    }

    public bool HasItem(int itemId)
    {
        return items.Contains(itemId);
    }

    private void Init()
    {
        LoadDefault();
    }

    private void LoadDefault()
    {
        items = new HashSet<int>();
        equipedItems = new Dictionary<ItemCategory, ItemData>();
        foreach (var item in defaultItems)
        {
            equipedItems.Add(item.category, item);
        }
    }

    public void UnlockItem(int id)
    {
        items.Add(id);
    }

    public void EquipItem(ItemData item)
    {
        if (equipedItems.ContainsKey(item.category))
        {
            if (localSkinContainer != null)
            {
                localSkinContainer.UnEquipSkin(equipedItems[item.category]);
            }
        }
        equipedItems[item.category] = item;
        if (localSkinContainer != null)
        {
            localSkinContainer.EquipSkin(item);
        }
    }
    public void UnEquipItem(ItemData item)
    {
        if (localSkinContainer != null)
        {
            localSkinContainer.UnEquipSkin(item);
        }
        equipedItems.Remove(item.category);
    }

    public int[] GetEquippedItemList()
    {
        int[] list = new int[equipedItems.Count];
        int i = 0;
        foreach (var item in equipedItems)
        {
            list[i] = item.Value.itemId;
            i++;
        }
        return list;
    }

    public bool isItemEquiped(ItemData item)
    {
        return equipedItems.ContainsKey(item.category) && equipedItems[item.category] == item;
    }
}
