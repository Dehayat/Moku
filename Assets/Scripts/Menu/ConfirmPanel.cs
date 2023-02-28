using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConfirmPanel : MonoBehaviour
{
    public static ConfirmPanel instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public TMPro.TextMeshProUGUI text;

    public Action confirmAction;
    public Action cancelAction;

    public void Confirm()
    {
        confirmAction?.Invoke();
        Clear();
    }

    private void Clear()
    {
        confirmAction = null;
        cancelAction = null;
        gameObject.SetActive(false);
    }

    public void Cancel()
    {
        cancelAction?.Invoke();
        Clear();
    }

    public void Show(string prompt, Action buy, Action cancel)
    {
        text.text = prompt;
        confirmAction = buy;
        cancelAction = cancel;
        gameObject.SetActive(true);
    }
}
