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

    private Animator anim;

    public void ResetAnim()
    {
        anim.SetBool("Shoot", false);
        anim.SetBool("Shield", false);
        anim.SetBool("Charge", false);
        anim.SetBool("Bomb", false);
    }

    public void Shoot()
    {
        ResetAnim();
        anim.SetBool("Shoot", true);
    }
    public void Shield()
    {
        ResetAnim();
        anim.SetBool("Shield", true);
    }
    public void Charge()
    {
        ResetAnim();
        anim.SetBool("Charge", true);
    }
    public void Bomb()
    {
        ResetAnim();
        anim.SetBool("Bomb", true);
    }

    [Networked(OnChanged = nameof(OnStateChanged))] public PlayerState currentState { get; set; }
    [Networked] public int lives { get; set; }
    [Networked] public int bullets { get; set; }

    public bool willBomb;
    public bool willShoot;
    public bool isShielding;

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
        if (Object.HasStateAuthority)
        {
            anim = GetComponentInChildren<Animator>();
            currentState = PlayerState.waiting;
            lives = 5;
            bullets = 0;
            FindAnyObjectByType<GameController>().RPC_PlayerJoin(this);
        }
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
}
