using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinContainer : MonoBehaviour
{
    public RectTransform contentContainer;
    public GameObject itemPrefab;
    public ItemData[] skinItems;

    private void OnEnable()
    {
        for (int i = 0; i < contentContainer.childCount; i++)
        {
            Destroy(contentContainer.GetChild(i).gameObject);
        }
        foreach (var item in skinItems)
        {
            if (GameData.instance.HasItem(item.itemId))
            {
                GameObject itemGO = Instantiate(itemPrefab, contentContainer);
                var itemButton = itemGO.GetComponent<SkinItemButton>();
                itemButton.Init(item);
            }
        }
    }
}
