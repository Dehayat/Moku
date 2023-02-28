using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Moku/ItemList")]
public class ItemList : ScriptableObject
{
    public ItemData[] allItems;

    private Dictionary<int, ItemData> itemMap;

    private void Init()
    {
        itemMap = new Dictionary<int, ItemData>();
        foreach (var item in allItems)
        {
            itemMap.Add(item.itemId, item);
        }
    }

    public ItemData GetItemById(int id)
    {
        if (itemMap == null)
        {
            Init();
        }
        return itemMap[id];
    }
}
