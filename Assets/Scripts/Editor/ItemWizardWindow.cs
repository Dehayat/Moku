using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ItemWizardWindow : EditorWindow
{

    int itemId = 10;
    Sprite itemIcon;
    ItemCategory itemCategory = ItemCategory.head;
    string fileName;
    ItemData currentItemData;
    float itemCost = 0.0f;
    ShopItem currentShopItemData;
    ItemList itemList;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Moku/Item Wizard")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ItemWizardWindow window = (ItemWizardWindow)EditorWindow.GetWindow(typeof(ItemWizardWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Create new Item", EditorStyles.boldLabel);
        fileName = EditorGUILayout.TextField("File name", fileName);
        itemId = EditorGUILayout.IntField("Item Id", itemId);
        itemIcon = (Sprite)EditorGUILayout.ObjectField("Item Icon", itemIcon, typeof(Sprite), false, null);
        itemCategory = (ItemCategory)EditorGUILayout.EnumPopup("Item category", itemCategory);
        if (GUILayout.Button("Create Skin Item"))
        {
            CreateItem();
        }
        EditorGUILayout.Space(30);
        GUILayout.Label("Create Shop Item from existing item", EditorStyles.boldLabel);
        currentItemData = (ItemData)EditorGUILayout.ObjectField("Item Data", currentItemData, typeof(ItemData), false, null);
        itemCost = EditorGUILayout.FloatField("Item cost", itemCost);
        if (GUILayout.Button("Create Shop Item"))
        {
            CreateShopItem();
        }
        EditorGUILayout.Space(30);
        GUILayout.Label("Manage Items", EditorStyles.boldLabel);
        itemList = (ItemList)EditorGUILayout.ObjectField("Item List", itemList, typeof(ItemList), false, null);
        if (GUILayout.Button("Add Item to Item List"))
        {
            AddToItemList();
        }
    }

    private void AddToItemList()
    {
        foreach (var item in itemList.allItems)
        {
            if (item == currentItemData)
            {
                Debug.LogError("Item already in list");
                return;
            }
        }
        var newItemList = new ItemData[itemList.allItems.Length + 1];
        itemList.allItems.CopyTo(newItemList, 0);
        newItemList[newItemList.Length - 1] = currentItemData;
        itemList.allItems = newItemList;
        AssetDatabase.SaveAssetIfDirty(itemList);
    }

    private void CreateItem()
    {
        ItemData itemFile = ScriptableObject.CreateInstance<ItemData>();
        string path = "Assets/Data/Skins/" + itemId + "_" + fileName + ".asset";
        AssetDatabase.CreateAsset(itemFile, path);
        itemFile.itemId = itemId;
        itemFile.itemIcon = itemIcon;
        itemFile.category = itemCategory;
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = itemFile;
        currentItemData = itemFile;
    }
    private void CreateShopItem()
    {
        ShopItem itemFile = ScriptableObject.CreateInstance<ShopItem>();
        string path = "Assets/Data/ShopItems/" + itemId + "_" + fileName + ".asset";
        AssetDatabase.CreateAsset(itemFile, path);
        itemFile.itemData = currentItemData;
        itemFile.itemCost = itemCost;
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = itemFile;
        currentShopItemData = itemFile;
    }
}
