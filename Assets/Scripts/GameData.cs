using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using System;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    private HashSet<int> items;

    private HashSet<int> itemsEquiped;
    private Dictionary<int, int> equipedCategoryItem;

    public SkinController localSkinContainer;

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
        itemsEquiped = new HashSet<int>();
        equipedCategoryItem = new Dictionary<int, int>();
        EquipItem(2, (int)SkinCategory.head);
    }

    public void UnlockItem(int id)
    {
        items.Add(id);
    }

    public void EquipItem(int id, int category)
    {
        itemsEquiped.Add(id);
        if (equipedCategoryItem.ContainsKey(category))
        {
            if (localSkinContainer != null)
            {
                localSkinContainer.UnEquipSkin(equipedCategoryItem[category], category);
            }
            itemsEquiped.Remove(equipedCategoryItem[category]);
        }
        equipedCategoryItem[category] = id;
        itemsEquiped.Add(id);
        if (localSkinContainer != null)
        {
            localSkinContainer.EquipSkin(id, category);
        }
    }
    public void UnEquipItem(int id, int category)
    {
        if (localSkinContainer != null)
        {
            localSkinContainer.UnEquipSkin(id, category);
        }
        itemsEquiped.Remove(id);
        equipedCategoryItem.Remove(category);
    }

    public bool isItemEquiped(int id)
    {
        return itemsEquiped.Contains(id);
    }
}
