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
    private int playerCount;
    private int playerChooseCount;

    public Player player1;
    public Player player2;

    private CombatMoves player1Choice;
    private CombatMoves player2Choice;

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            currentState = GameState.WaitingForPlayersToJoin;
            playerCount = 0;
        }
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)]
    public void RPC_PlayerJoin(Player player, RpcInfo info = default)
    {
        playerCount++;
        if (playerCount == 2)
        {
            player2 = player;
            combatResolver.SetPlayers(player1, player2);
            Debug.Log("All players are here");
            SetUpRound();
        }
        else
        {
            player1 = player;
        }
    }

    private void SetUpRound()
    {
        //Setup Round
        currentState = GameState.WaitingForPlayersToChoose;
        playerChooseCount = 0;
        foreach (var playerInstance in GameObject.FindObjectsOfType<Player>())
        {
            playerInstance.currentState = PlayerState.choosing;
        }
        player1Choice = CombatMoves.nothing;
        player2Choice = CombatMoves.nothing;
        //End Setup round
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)]
    public void RPC_Choose(CombatMoves move, Player player, RpcInfo info = default)
    {
        Debug.Log(info.Source.PlayerId + " Chose " + move.ToString());
        player.currentState = PlayerState.chosen;
        playerChooseCount++;
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
            //Resolve combat
            currentState = GameState.ResolvingChoices;
            Debug.Log("Resolving choices");
            combatResolver.ResolveCombat(player1Choice, player2Choice);
            //go to next turn
            SetUpRound();
        }
    }
}
