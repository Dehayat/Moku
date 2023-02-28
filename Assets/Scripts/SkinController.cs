using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[System.Serializable]
public struct SkinData
{
    public ItemData itemData;
    public SkinBehaviour skinBehaviour;
}

public class SkinController : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnPlayerSkinChanged))] public int head { get; set; } = 0;
    [Networked(OnChanged = nameof(OnPlayerSkinChanged))] public int body { get; set; } = 0;
    [Networked(OnChanged = nameof(OnPlayerSkinChanged))] public int arms { get; set; } = 0;
    [Networked(OnChanged = nameof(OnPlayerSkinChanged))] public int overHead { get; set; } = 0;
    [Networked(OnChanged = nameof(OnPlayerSkinChanged))] public int overBody { get; set; } = 0;
    [Networked(OnChanged = nameof(OnPlayerSkinChanged))] public int overArms { get; set; } = 0;
    [Networked(OnChanged = nameof(OnPlayerSkinChanged))] public int voice { get; set; } = 0;

    public SkinData[] skinDataList;

    private Dictionary<int, SkinData> skinDataMap;

    public static void OnPlayerSkinChanged(Changed<SkinController> changed)
    {
        changed.LoadOld();
        changed.Behaviour.RemoveOldSkin();
        changed.LoadNew();
        changed.Behaviour.AddNewSkin();
    }

    public void AddNewSkin()
    {
        if (body != 0)
        {
            skinDataMap[body].skinBehaviour.Equip();
        }
        if (head != 0)
        {
            skinDataMap[head].skinBehaviour.Equip();
        }
        if (arms != 0)
        {
            skinDataMap[arms].skinBehaviour.Equip();
        }
        if (overBody != 0)
        {
            skinDataMap[overBody].skinBehaviour.Equip();
        }
        if (overHead != 0)
        {
            skinDataMap[overHead].skinBehaviour.Equip();
        }
        if (overArms != 0)
        {
            skinDataMap[overArms].skinBehaviour.Equip();
        }
        if (voice != 0)
        {
            skinDataMap[voice].skinBehaviour.Equip();
        }
    }

    public void RemoveOldSkin()
    {
        if (body != 0)
        {
            skinDataMap[body].skinBehaviour.UnEquip();
        }
        if (head != 0)
        {
            skinDataMap[head].skinBehaviour.UnEquip();
        }
        if (arms != 0)
        {
            skinDataMap[arms].skinBehaviour.UnEquip();
        }
        if (overBody != 0)
        {
            skinDataMap[overBody].skinBehaviour.Equip();
        }
        if (overHead != 0)
        {
            skinDataMap[overHead].skinBehaviour.UnEquip();
        }
        if (overArms != 0)
        {
            skinDataMap[overArms].skinBehaviour.UnEquip();
        }
        if (voice != 0)
        {
            skinDataMap[voice].skinBehaviour.UnEquip();
        }
    }

    private void Awake()
    {
        skinDataMap = new Dictionary<int, SkinData>();
        foreach (var item in skinDataList)
        {
            skinDataMap.Add(item.itemData.itemId, item);
        }
    }

    private void Start()
    {
        if (GameData.instance != null && !(Runner != null && Runner.IsRunning))
        {
            GameData.instance.SetLocalSkinController(this);
        }
    }

    public void EquipSkin(ItemData item)
    {
        if (Runner == null || !Runner.IsRunning)
        {
            skinDataMap[item.itemId].skinBehaviour.Equip();
            return;
        }
        switch (item.category)
        {
            case ItemCategory.head:
                head = item.itemId;
                break;
            case ItemCategory.arm:
                arms = item.itemId;
                break;
            case ItemCategory.body:
                body = item.itemId;
                break;
            case ItemCategory.overHead:
                overHead = item.itemId;
                break;
            case ItemCategory.overBody:
                overBody = item.itemId;
                break;
            case ItemCategory.overArm:
                overArms = item.itemId;
                break;
            case ItemCategory.voice:
                voice = item.itemId;
                break;
            default:
                break;
        }
    }
    public void UnEquipSkin(ItemData item)
    {
        if (Runner == null || !Runner.IsRunning)
        {
            skinDataMap[item.itemId].skinBehaviour.UnEquip();
            return;
        }
        switch (item.category)
        {
            case ItemCategory.head:
                head = 0;
                break;
            case ItemCategory.arm:
                arms = 0;
                break;
            case ItemCategory.body:
                body = 0;
                break;
            case ItemCategory.overHead:
                overHead = 0;
                break;
            case ItemCategory.overBody:
                overBody = 0;
                break;
            case ItemCategory.overArm:
                overArms = 0;
                break;
            case ItemCategory.voice:
                voice = 0;
                break;
            default:
                break;
        }
    }
}
