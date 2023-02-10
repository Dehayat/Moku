using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GameController : NetworkBehaviour
{

    [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)]
    public void RPC_Configure(RpcInfo info = default)
    {
        Debug.Log(info.Source.PlayerId);
    }
    [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)]
    public void RPC_Choose(string choice, RpcInfo info = default)
    {
        Debug.Log(info.Source.PlayerId + " Chose " + choice);
    }
}
