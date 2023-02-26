using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

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

    [Networked(OnChanged = nameof(OnStateChanged))] public PlayerState currentState { get; set; }
    [Networked] public int lives { get; set; }
    [Networked] public int bullets { get; set; }
    [Networked(OnChanged = nameof(OnPlayerViewChanged))] public PlayerView view { get; set; }

    public bool willBomb;
    public bool willShoot;
    public bool isShielding;

    public override void Spawned()
    {
        //Debug.Log("this is coming from " + Runner.LocalPlayer.PlayerId);
        //RPC_Test();

        if (Object.HasStateAuthority)
        {
            currentState = PlayerState.waiting;
            lives = 5;
            bullets = 0;
            FindAnyObjectByType<GameController>().PlayerJoin(this);
        }
    }

    public static void OnStateChanged(Changed<Player> changed)
    {
        changed.Behaviour.OnStateChanged();
    }
    public static void OnPlayerViewChanged(Changed<Player> changed)
    {
        changed.Behaviour.OnPlayerViewChanged();
    }
    private void OnPlayerViewChanged()
    {
        if (view != null)
        {
            var spawnpoints = FindAnyObjectByType<SpawnPointHolder>();
            var ownerSpawn = spawnpoints.spawnPointOwner.position;
            var enemySpawn = spawnpoints.spawnPointEnemy.position;
            if (HasInputAuthority)
            {
                view.transform.position = ownerSpawn;
                var scale = view.transform.localScale;
                scale.x = -1;
                view.transform.localScale = scale;
            }
            else
            {
                view.transform.position = enemySpawn;
            }
        }
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

    [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    public void RPC_Test(RpcInfo info = default)
    {
        Debug.Log("it did the thing" + info.Source.PlayerId);
    }


    public void ResetRound()
    {
        bullets = 0;
    }

    public override void FixedUpdateNetwork()
    {
    }
    private void OnGUI()
    {
        if (Object.HasInputAuthority && currentState == PlayerState.choosing)
        {

            if (GUI.Button(new Rect(0, 0, 200, 40), "Charge"))
            {
                FindAnyObjectByType<GameController>().RPC_Choose(CombatMoves.charge, this);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Shield"))
            {
                FindAnyObjectByType<GameController>().RPC_Choose(CombatMoves.shield, this);
            }
            if (GUI.Button(new Rect(0, 80, 200, 40), "Shoot"))
            {
                FindAnyObjectByType<GameController>().RPC_Choose(CombatMoves.shoot, this);
            }
            if (GUI.Button(new Rect(0, 120, 200, 40), "Bomb"))
            {
                FindAnyObjectByType<GameController>().RPC_Choose(CombatMoves.bomb, this);
            }
        }
    }

    public string GetPlayerId()
    {
        return "Player " + Object.InputAuthority.PlayerId;
    }
}
