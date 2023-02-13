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
    private GameController gameController;

    public void Init(GameController gameController)
    {
        this.gameController = gameController;
    }
    public void SetPlayers(Player player1, Player player2)
    {
        this.player1 = player1;
        this.player2 = player2;
    }

    public void ResolveCombat(CombatMoves player1Choice, CombatMoves player2Choice)
    {
        StartCoroutine(CombatSequence(player1Choice, player2Choice));
    }

    IEnumerator CombatSequence(CombatMoves player1Choice, CombatMoves player2Choice)
    {
        ResolvePlayerChoice(player1, player1Choice);
        ResolvePlayerChoice(player2, player2Choice);
        yield return new WaitForSecondsRealtime(1);
        ResolveInteraction(player1Choice, player2Choice);
        yield return new WaitForSecondsRealtime(1);
        player1.view.ResetAnim();
        player2.view.ResetAnim();
        yield return new WaitForSecondsRealtime(1);
        gameController.EndTurn();
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
        else if (player2Choice == CombatMoves.shoot && player2.willShoot)
        {
            ShootOtherPlayer(player2, player1);
        }
    }

    private void BombOtherPlayer(Player bomberPlayer, Player bombedPlayer)
    {
        Debug.Log(bomberPlayer.Id + " Bombed " + bombedPlayer.Id);
        bombedPlayer.lives--;
        ResetRound();
    }

    private void ResetRound()
    {
        player1.ResetRound();
        player2.ResetRound();
    }

    private void ShootOtherPlayer(Player shootingPlayer, Player shotPlayer)
    {
        Debug.Log(shootingPlayer.Id + " Shot" + shotPlayer.Id);
        if (shotPlayer.isShielding)
        {
            Debug.Log("But " + shotPlayer.Id + " Is shielding");
        }
        else
        {
            Debug.Log("And it hurt " + shotPlayer.GetPlayerId());
            shotPlayer.lives--;
            ResetRound();
        }
    }

    private void ShootEachOther()
    {
        Debug.Log("Both players shot each other");
        player1.lives--;
        player2.lives--;
        ResetRound();
    }

    private void ResolvePlayerChoice(Player player, CombatMoves choice)
    {
        player.willBomb = false;
        player.willShoot = false;
        player.isShielding = false;
        if (choice == CombatMoves.nothing)
        {
            Debug.Log(player.GetPlayerId() + " Did Nothing");
        }
        else if (choice == CombatMoves.charge)
        {
            player.view.Charge();
            Debug.Log(player.GetPlayerId() + " Charging");
            player.bullets += 1;
        }
        else if (choice == CombatMoves.shield)
        {
            player.view.Shield();
            Debug.Log(player.GetPlayerId() + " Shielding");
            player.isShielding = true;
        }
        else if (choice == CombatMoves.shoot)
        {
            player.view.Shoot();
            if (player.bullets >= 1)
            {
                Debug.Log(player.GetPlayerId() + " shooting");
                player.bullets -= 1;
                player.willShoot = true;
            }
            else
            {
                Debug.Log(player.GetPlayerId() + " tried to shoot but has no bullets");
            }
        }
        else if (choice == CombatMoves.bomb)
        {
            if (player.bullets >= 5)
            {
                player.view.Bomb();
                Debug.Log(player.GetPlayerId() + " bombing");
                player.bullets -= 5;
                player.willBomb = true;
            }
            else
            {
                Debug.Log(player.GetPlayerId() + " tried to bomb but has no bullets");
            }
        }
    }
}
