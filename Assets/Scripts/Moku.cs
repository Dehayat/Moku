using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moku : MonoBehaviour
{

    private int voiceId;

    private void Start()
    {
        var voice = GameData.instance.GetEquippedItem(ItemCategory.voice);
        voiceId = voice.itemId;
    }

    public void PlaySound()
    {
        FindObjectOfType<GameController>().RPC_PlaySound(voiceId);
    }
}
