using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinContainer : MonoBehaviour
{
    public RectTransform contentContainer;
    public GameObject itemPrefab;
    public ItemData[] skinItems;

    private void Start()
    {
        foreach (var item in skinItems)
        {
            GameObject itemGO = Instantiate(itemPrefab, contentContainer);
            var itemButton = itemGO.GetComponent<SkinItemButton>();
            itemButton.Init(item);
        }
    }
}
