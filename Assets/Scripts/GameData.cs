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
    private List<int> unlockedItemsSave;
    private List<int> equipedItemsSave;
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
        foreach (var item in defaultItems)
        {
            equipedItems.Add(item.category, item);
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
        equipedItemsSave = new List<int>();
        foreach (var item in equipedItems)
        {
            equipedItemsSave.Add(item.Value.itemId);
        }
        unlockedItemsSave = new List<int>();
        foreach (var item in unlockedItems)
        {
            unlockedItemsSave.Add(item);
        }
        PlayerPrefs.SetString("unlocksList", JsonConvert.SerializeObject(unlockedItems));
        PlayerPrefs.SetString("equippedList", JsonConvert.SerializeObject(equipedItemsSave));
        PlayerPrefs.SetInt("saved", 1);
        PlayerPrefs.Save();
    }
    private void Load()
    {
        unlockedItemsSave = JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString("unlocksList"));
        equipedItemsSave = JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString("equippedList"));
        equipedItems = new Dictionary<ItemCategory, ItemData>();
        foreach (var item in equipedItemsSave)
        {
            var itemData = GetItemById(item);
            equipedItems.Add(itemData.category, itemData);
        }
        unlockedItems = new HashSet<int>();
        foreach (var item in unlockedItemsSave)
        {
            unlockedItems.Add(item);
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
