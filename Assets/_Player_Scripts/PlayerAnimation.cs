using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    void Start()
    {

    }


    void Update()
    {
        anim.SetFloat("WalkFB",PlayerState.Instance.Moving);
    }
}