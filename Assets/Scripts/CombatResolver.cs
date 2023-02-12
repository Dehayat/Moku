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
        ResolvePlayerChoice(player2, player2Choice);
        ResolveInteraction(player1Choice, player2Choice);
    }

    private void ResolveInteraction(CombatMoves player1Choice, CombatMoves player2Choice)
    {
        //Bomb
        if (player1Choice == CombatMoves.bomb && player1.willBomb)
        {
            BombOtherPlayer(player1, player2);
        }
        else if (player2Choice == CombatMoves.bomb && player2.willBomb)
        {
            BombOtherPlayer(player2, player1);
        }
        //Shoot
        else if (player1Choice == CombatMoves.shoot && player1.willShoot)
        {
            if (player2Choice == CombatMoves.shoot && player2.willShoot)
            {
                ShootEachOther();
            }
            else
            {
                ShootOtherPlayer(player1, player2);
            }
        }
        else if (player2Choice == CombatMoves.shoot)
        {
            ShootOtherPlayer(player2, player1);
        }
    }

    private void BombOtherPlayer(Player bomberPlayer, Player bombedPlayer)
    {
        //Play bomb animation regardless
        Debug.Log(bomberPlayer.Id + " Bombed " + bombedPlayer.Id);
        bombedPlayer.lives--;
    }

    private void ShootOtherPlayer(Player shootingPlayer, Player shotPlayer)
    {
        //play get shoot animation
        Debug.Log(shootingPlayer.Id + " Shot" + shotPlayer.Id);
        if (shotPlayer.isShielding)
        {
            Debug.Log("But " + shotPlayer.Id + " Is shielding");
        }
        else
        {
            Debug.Log("And it hurt " + shotPlayer.Id);
            shotPlayer.lives--;
        }
    }

    private void ShootEachOther()
    {
        //play both get shot animation
        Debug.Log("Both players shot each other");
    }

    private void ResolvePlayerChoice(Player player, CombatMoves choice)
    {
        player.willBomb = false;
        player.willShoot = false;
        player.isShielding = false;
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
            player.isShielding = true;
        }
        else if (choice == CombatMoves.shoot)
        {
            //playe shoot animaiton
            if (player.bullets >= 1)
            {
                Debug.Log(player.Id + " shooting");
                player.bullets -= 1;
                player.willShoot = true;
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
                player.willBomb = true;
            }
            else
            {
                Debug.Log(player.Id + " Failed to bomb");
            }
        }
    }
}
