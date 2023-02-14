using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerView : NetworkBehaviour
{

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }


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
}
