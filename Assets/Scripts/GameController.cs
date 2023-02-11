using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

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
    private GameState currentState;
    private int playerCount;
    private int playerChooseCount;

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            currentState = GameState.WaitingForPlayersToJoin;
            playerCount = 0;
        }
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)]
    public void RPC_PlayerJoin(RpcInfo info = default)
    {
        playerCount++;
        if (playerCount == 2)
        {
            currentState = GameState.WaitingForPlayersToChoose;
            playerChooseCount = 0;
        }
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)]
    public void RPC_Choose(string choice, Player player, RpcInfo info = default)
    {
        Debug.Log(info.Source.PlayerId + " Chose " + choice);
        player.currentState = PlayerState.chosen;
        playerChooseCount++;
        if (playerChooseCount == 2)
        {
            currentState = GameState.ResolvingChoices;
            Debug.Log("Resolving choices");
        }
    }
}
