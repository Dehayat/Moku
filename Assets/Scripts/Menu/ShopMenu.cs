using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public RectTransform contentContainer;
    public GameObject itemPrefab;
    public ShopItem[] shopItems;

    private void Start()
    {
        foreach (var item in shopItems)
        {
            GameObject itemGO = Instantiate(itemPrefab, contentContainer);
            //itemGO.GetComponent<RectTransform>().SetParent(contentContainer);
            itemGO.name = item.itemId + " / " + item.itemCost;
            var itemButton = itemGO.GetComponent<ShopItemButton>();
            itemButton.Init(item);
        }
    }
}
