using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    public override void Spawned()
    {
        FindAnyObjectByType<GameController>().RPC_Configure();
    }
}
