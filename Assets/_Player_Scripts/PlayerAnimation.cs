using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetBool("KnifeEquipped", GunManager.Instance.WeaponEquipped == "Knife");
        anim.SetBool("PistolEquipped", GunManager.Instance.WeaponEquipped == "Pistol");
        anim.SetBool("RifleEquipped", GunManager.Instance.WeaponEquipped == "Rifle");
        anim.SetBool("ShotgunEquipped", GunManager.Instance.WeaponEquipped == "Shotgun");
        anim.SetFloat("WalkFB",PlayerState.Instance.MovingZ);
        anim.SetFloat("WalkSR",PlayerState.Instance.MovingX);
        anim.SetBool("Aiming",PlayerState.Instance.Aiming);
        anim.SetBool("Running",PlayerState.Instance.Running);
    }
}