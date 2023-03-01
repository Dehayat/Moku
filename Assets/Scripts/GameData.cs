using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class GameData : MonoBehaviour
{
    public static GameData instance;


    public Player localPlayer;

    public ItemList itemList;
    public SkinController localSkinContainer;
    public ItemData[] defaultItems;
    public ItemData[] defaultUnlockedItems;

    public bool deleteAll = false;

    private HashSet<int> unlockedItems;
    private Dictionary<ItemCategory, int> equipedItemsSave;
    private Dictionary<ItemCategory, ItemData> equipedItems;


    public ItemData GetItemById(int id)
    {
        return itemList.GetItemById(id);
    }
    public ItemData GetEquippedItem(ItemCategory voice)
    {
        return equipedItems[voice];
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

    private void Update()
    {
        if (deleteAll)
        {
            deleteAll = false;
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
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
        return unlockedItems.Contains(itemId);
    }

    private void Init()
    {
        if (PlayerPrefs.GetInt("saved", -1) != -1)
        {
            Load();
        }
        else
        {
            LoadDefault();
        }
    }

    private void LoadDefault()
    {
        unlockedItems = new HashSet<int>();
        equipedItems = new Dictionary<ItemCategory, ItemData>();
        equipedItemsSave = new Dictionary<ItemCategory, int>();
        foreach (var item in defaultItems)
        {
            equipedItems.Add(item.category, item);
            equipedItemsSave.Add(item.category, item.itemId);
        }
        foreach (var item in defaultUnlockedItems)
        {
            unlockedItems.Add(item.itemId);
        }
    }

    public void UnlockItem(int id)
    {
        unlockedItems.Add(id);
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetString("unlocks", JsonConvert.SerializeObject(unlockedItems));
        PlayerPrefs.SetString("equipped", JsonConvert.SerializeObject(equipedItemsSave));
        PlayerPrefs.SetInt("saved", 1);
        PlayerPrefs.Save();
    }
    private void Load()
    {
        unlockedItems = JsonConvert.DeserializeObject<HashSet<int>>(PlayerPrefs.GetString("unlocks"));
        equipedItemsSave = JsonConvert.DeserializeObject<Dictionary<ItemCategory, int>>(PlayerPrefs.GetString("equipped"));
        equipedItems = new Dictionary<ItemCategory, ItemData>();
        foreach (var item in equipedItemsSave)
        {
            equipedItems.Add(item.Key, GetItemById(item.Value));
        }
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
        equipedItemsSave[item.category] = item.itemId;
        if (localSkinContainer != null)
        {
            localSkinContainer.EquipSkin(item);
        }
        Save();
    }
    public void UnEquipItem(ItemData item)
    {
        if (localSkinContainer != null)
        {
            localSkinContainer.UnEquipSkin(item);
        }
        equipedItems.Remove(item.category);
        equipedItemsSave.Remove(item.category);
        Save();
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
