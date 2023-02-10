using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            FindAnyObjectByType<GameController>().RPC_Configure();
        }
    }
    public override void FixedUpdateNetwork()
    {
    }
    private void OnGUI()
    {
        if (Object.HasInputAuthority)
        {

            if (GUI.Button(new Rect(0, 0, 200, 40), "Rock"))
            {
                FindAnyObjectByType<GameController>().RPC_Choose("Rock");
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Paper"))
            {
                FindAnyObjectByType<GameController>().RPC_Choose("Paper");
            }
        }
    }
}
