using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using System;

public class GameUI : NetworkBehaviour
{

    public Text playerOwnerHealth;
    public Text playerOwnerBullets;
    public Text playerEnemyHealth;
    public Text playerEnemyBullets;
    public Text timerText;

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
        playerOwnerBullets.text = "Bullets: " + player1.bullets;
        playerEnemyHealth.text = "Health: " + player2.lives;
        playerEnemyBullets.text = "Bullets: " + player2.bullets;
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
}
