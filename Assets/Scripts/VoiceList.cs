using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct voiceData
{
    public int itemId;
    public AudioClip clip;
}

public class VoiceList : MonoBehaviour
{
    public static VoiceList instance;

    private void Awake()
    {
        instance = this;
    }

    public voiceData[] voices;

    public AudioClip GetVoiceById(int id)
    {
        foreach (var item in voices)
        {
            if (item.itemId == id)
            {
                return item.clip;
            }
        }
        return null;
    }
}
