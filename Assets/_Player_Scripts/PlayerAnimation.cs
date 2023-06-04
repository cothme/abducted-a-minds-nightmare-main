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
        anim.SetBool("HandgunReload", PlayerState.Instance.Reloading);
        anim.SetBool("Running", PlayerState.Instance.Running);
    }
}