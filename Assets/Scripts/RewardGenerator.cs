using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardGenerator : MonoBehaviour
{
    public float winRewardChance = 1;
    public float loseRewardChance = 0.5f;

    public ItemData TryUnlockReward(bool win)
    {
        float rewardChance = loseRewardChance;
        if (win)
        {
            rewardChance = winRewardChance;
        }
        if (Random.Range(0f, 1f) < rewardChance)
        {
            List<int> unlockableItems = new List<int>();
            foreach (var item in GameData.instance.itemList.allItems)
            {
                if (!GameData.instance.HasItem(item.itemId))
                {
                    unlockableItems.Add(item.itemId);
                }
            }
            if (unlockableItems.Count == 0)
            {
                return null;
            }
            else
            {
                int unlock = Random.Range(0, unlockableItems.Count);
                GameData.instance.UnlockItem(unlockableItems[unlock]);
                return GameData.instance.GetItemById(unlockableItems[unlock]);
            }
        }
        else
        {
            return null;
        }
    }
}
