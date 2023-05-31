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
        Debug.Log(PlayerState.Instance.MovingX + " " + PlayerState.Instance.MovingZ);
        anim.SetFloat("WalkFB",PlayerState.Instance.MovingZ);
        anim.SetFloat("WalkSR",PlayerState.Instance.MovingX);
    }
}