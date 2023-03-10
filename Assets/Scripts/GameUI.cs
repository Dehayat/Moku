using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using System;
using TMPro;

public class GameUI : NetworkBehaviour
{

    public Text playerOwnerHealth;
    public HealthUI ownerHealthUI;
    public Text playerOwnerBullets;
    public Text playerEnemyHealth;
    public HealthUI enemyHealthUI;
    public Text playerEnemyBullets;
    public Text timerText;

    public GameObject loadingScreen;
    public GameObject endScreen;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI rewardText;
    public Image rewardIcon;
    public string winText;
    public string loseText;
    public GameObject mokuButton;

    [Networked(OnChanged = nameof(OnTimerChanged))] private int timerTime { get; set; }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_UpdateUI(Player player1, Player player2)
    {
        if (player2.HasInputAuthority)
        {
            var temp = player1;
            player1 = player2;
            player2 = temp;
        }

        playerOwnerHealth.text = "Health: " + player1.lives;
        ownerHealthUI.SetHealth(player1.lives);
        playerOwnerBullets.text = "Bullets: " + player1.bullets;
        playerEnemyHealth.text = "Health: " + player2.lives;
        playerEnemyBullets.text = "Bullets: " + player2.bullets;
        enemyHealthUI.SetHealth(player2.lives);
    }
    public static void OnTimerChanged(Changed<GameUI> changed)
    {
        changed.Behaviour.OnTimerChanged();
    }
    public void OnTimerChanged()
    {
        if (timerTime < 0)
        {
            timerText.text = "";
        }
        else
        {
            timerText.text = timerTime.ToString();
        }
    }


    public void UpdateTimer(int t)
    {
        timerTime = t;
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_HideLoading()
    {
        loadingScreen.SetActive(false);
    }

    public void ShowEndScreen(bool win, ItemData reward)
    {
        if (win)
        {
            resultText.text = winText;
            mokuButton.SetActive(true);
        }
        else
        {
            resultText.text = loseText;
            mokuButton.SetActive(false);
        }
        if (reward == null)
        {
            rewardText.text = "Yo got nothing :))";
            rewardIcon.gameObject.SetActive(false);
        }
        else
        {
            rewardText.text = "Unlocked";
            rewardIcon.sprite = reward.itemIcon;
            rewardIcon.gameObject.SetActive(true);
        }
        endScreen.SetActive(true);
    }
}
