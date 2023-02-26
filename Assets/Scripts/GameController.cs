using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public enum GameState
{
    WaitingForPlayersToJoin,
    WaitingForPlayersToChoose,
    ResolvingChoices
}

/// <summary>
/// This is the main controller and only works on the host
/// </summary>
public class GameController : NetworkBehaviour
{
    [SerializeField]
    private CombatResolver combatResolver;
    [SerializeField]
    private GameState currentState;
    [SerializeField]
    private GameObject playerViewPrefab;
    [SerializeField]
    private GameUI ui;
    private int playerCount;
    private int playerChooseCount;

    [SerializeField]
    private int chooseTime = 5;

    public Player player1;
    public Player player2;

    private CombatMoves player1Choice;
    private CombatMoves player2Choice;

    int playersConnected = 0;
    public override void Spawned()
    {
        playersConnected++;
        if (Object.HasStateAuthority)
        {
            currentState = GameState.WaitingForPlayersToJoin;
            playerCount = 0;
            combatResolver.Init(this);
        }
    }


    public void PlayerJoin(Player player)
    {
        var obj = Runner.Spawn(playerViewPrefab, player.transform.position);
        player.view = obj.GetComponent<PlayerView>();
        playerCount++;
        if (playerCount == 2)
        {
            player2 = player;
            combatResolver.SetPlayers(player1, player2);
            Debug.Log("All players are here");
            StartCoroutine(WaitForPlayersToBeReady());
        }
        else
        {
            player1 = player;
        }
    }

    IEnumerator WaitForPlayersToBeReady()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        ui.RPC_UpdateUI(player1, player2);
        GoToChooseState();
    }

    private void SetUpRound()
    {
        currentState = GameState.WaitingForPlayersToChoose;
        playerChooseCount = 0;
        foreach (var playerInstance in GameObject.FindObjectsOfType<Player>())
        {
            playerInstance.currentState = PlayerState.choosing;
        }
        player1Choice = CombatMoves.nothing;
        player2Choice = CombatMoves.nothing;
        StartCoroutine(ChooseTimerSequence());
    }
    IEnumerator ChooseTimerSequence()
    {
        ui.UpdateTimer(-1);
        int t = chooseTime;
        ui.UpdateTimer(t);
        while (t > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            t--;
            ui.UpdateTimer(t);
            if (currentState != GameState.WaitingForPlayersToChoose)
            {
                ui.UpdateTimer(-1);
                break;
            }
        }
        if (currentState == GameState.WaitingForPlayersToChoose)
        {
            ui.UpdateTimer(-1);
            player1.currentState = PlayerState.chosen;
            player2.currentState = PlayerState.chosen;
            GoToResolveState();
        }
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)]
    public void RPC_Choose(CombatMoves move, Player player, RpcInfo info = default)
    {
        if (currentState != GameState.WaitingForPlayersToChoose)
        {
            return;
        }
        Debug.Log(info.Source.PlayerId + " Chose " + move.ToString());
        player.currentState = PlayerState.chosen;
        playerChooseCount++;
        //Save player choice
        if (player == player1)
        {
            player1Choice = move;
        }
        else
        {
            player2Choice = move;
        }
        //Both players chosen
        if (playerChooseCount == 2)
        {
            GoToResolveState();
        }
    }

    private void GoToResolveState()
    {
        //Resolve combat
        currentState = GameState.ResolvingChoices;
        Debug.Log("Resolving choices");
        combatResolver.ResolveCombat(player1Choice, player2Choice);
    }

    public void EndTurn()
    {
        ui.RPC_UpdateUI(player1, player2);
        Debug.Log("============== Round Ended ====================");
        if (player1.lives > 0 && player2.lives > 0)
        {
            GoToChooseState();
        }
    }

    private void GoToChooseState()
    {
        Debug.Log("=============== Round Start ===================");
        SetUpRound();
    }
}
