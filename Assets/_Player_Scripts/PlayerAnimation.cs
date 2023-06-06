using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetFloat("WalkFB",PlayerState.Instance.MovingZ);
        anim.SetFloat("WalkSR",PlayerState.Instance.MovingX);
        anim.SetBool("HGReload", PlayerState.Instance.Reloading);
        anim.SetBool("Running", PlayerState.Instance.Running);
        anim.SetBool("Aiming",PlayerState.Instance.Aiming);
        anim.SetBool("Shooting",gameObject.GetComponent<PlayerShootingScript>().isShooting);
    }
}