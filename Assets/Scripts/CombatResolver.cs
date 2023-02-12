using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public enum CombatMoves
{
    nothing,
    shield,
    charge,
    shoot,
    bomb
}

public class CombatResolver : NetworkBehaviour
{
    public Player player1;
    public Player player2;

    public void SetPlayers(Player player1, Player player2)
    {
        this.player1 = player1;
        this.player2 = player2;
    }

    public void ResolveCombat(CombatMoves player1Choice, CombatMoves player2Choice)
    {
        ResolvePlayerChoice(player1, player1Choice);
    }

    private void ResolvePlayerChoice(Player player, CombatMoves choice)
    {
        if (choice == CombatMoves.nothing)
        {

            Debug.Log(player.Id + " Did Nothing");
        }
        else if (choice == CombatMoves.charge)
        {
            //play charge animation
            Debug.Log(player.Id + " Charging");
            player.bullets += 1;
        }
        else if (choice == CombatMoves.shield)
        {
            //playe shield animation
            Debug.Log(player.Id + " Shielding");
        }
        else if (choice == CombatMoves.shoot)
        {
            //playe shoot animaiton
            if (player.bullets >= 1)
            {
                Debug.Log(player.Id + " shooting");
                player.bullets -= 1;
            }
            else
            {
                Debug.Log(player.Id + " Failed to shoot");
            }
        }
        else if (choice == CombatMoves.bomb)
        {
            //playe shoot animaiton
            if (player.bullets >= 5)
            {
                Debug.Log(player.Id + " bombing");
                player.bullets -= 5;
            }
            else
            {
                Debug.Log(player.Id + " Failed to bomb");
            }
        }
    }
}
