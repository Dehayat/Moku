using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUI : MonoBehaviour
{
    public GameController game;
    public GameObject actionsContainer;

    public void ShowActions()
    {
        actionsContainer.SetActive(true);
    }
    public void HideActions()
    {
        actionsContainer.SetActive(false);
    }

    public void Shoot()
    {
        Choose(CombatMoves.shoot);
    }
    public void Shield()
    {
        Choose(CombatMoves.shield);
    }
    public void Bomb()
    {
        Choose(CombatMoves.bomb);
    }
    public void Charge()
    {
        Choose(CombatMoves.charge);
    }

    public void Choose(CombatMoves move)
    {
        game.RPC_Choose(move, GameData.instance.localPlayer);
    }
}
