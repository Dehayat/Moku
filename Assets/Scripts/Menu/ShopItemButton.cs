using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemButton : MonoBehaviour
{
    public GameObject disableOverlay;
    public TextMeshProUGUI priceText;
    public Image icon;

    private ShopItem item;


    public void Init(ShopItem item)
    {
        this.item = item;
        UpdateView();
    }

    private void UpdateView()
    {
        priceText.text = item.itemCost + "$";
        icon.sprite = item.itemIcon;

        if (GameData.instance.HasItem(item.itemId))
        {
            disableOverlay.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
        else
        {
            disableOverlay.SetActive(false);
            GetComponent<Button>().interactable = true;
        }
    }

    public void BuyItem()
    {
        GameData.instance.UnlockItem(item.itemId);
        UpdateView();
    }
}
