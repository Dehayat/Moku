using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public enum PlayerState
{
    waiting,
    choosing,
    chosen
}
/// <summary>
/// This is used on every client but each client does their own shit
/// </summary>
public class Player : NetworkBehaviour
{

    [Networked(OnChanged = nameof(OnStateChanged))] public PlayerState currentState { get; set; } = PlayerState.choosing;

    public static void OnStateChanged(Changed<Player> changed)
    {
        changed.Behaviour.OnStateChanged();
    }

    private void OnStateChanged()
    {
        if (currentState == PlayerState.waiting)
        {
        }
        if (currentState == PlayerState.choosing)
        {
            //allow choosing
            //show ui
        }
        if (currentState == PlayerState.chosen)
        {
            //dont allow choosing
            //Remove gui i guess
        }
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            FindAnyObjectByType<GameController>().RPC_PlayerJoin();
            currentState = PlayerState.waiting;
        }
    }
    public override void FixedUpdateNetwork()
    {
    }
    private void OnGUI()
    {
        if (Object.HasInputAuthority && currentState == PlayerState.choosing)
        {

            if (GUI.Button(new Rect(0, 0, 200, 40), "Rock"))
            {
                FindAnyObjectByType<GameController>().RPC_Choose("Rock", this);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Paper"))
            {
                FindAnyObjectByType<GameController>().RPC_Choose("Paper", this);
            }
        }
    }
}
