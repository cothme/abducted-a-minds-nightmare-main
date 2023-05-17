using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerShootingScript : MonoBehaviour
{
    [SerializeField] ParticleSystem playerParticleSystem;
    [SerializeField] Transform cameraTransform;
    [SerializeField] GameObject bulletPreFab;
    [SerializeField] Transform bulletTransform;
    [SerializeField] float bulletMissDistance = 25f;
    PlayerControls playerControls;
    InputAction fireButton;
    InputAction reloadButton;
    InputAction rifleButton,shotgunButton,pistolButton,knifeButton;
    #region Input Setup
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
     private void OnEnable()
    {
        rifleButton = playerControls.Weapon.Weapon1;
        shotgunButton = playerControls.Weapon.Weapon2;
        pistolButton = playerControls.Weapon.Weapon3;
        knifeButton = playerControls.Weapon.Weapon4;
        fireButton = playerControls.Player.FireSingleBullet;
        fireButton.Enable();
        fireButton.performed += Shoot;
        reloadButton = playerControls.Player.Reload;
        reloadButton.performed += Reload;
        reloadButton.Enable();
        rifleButton.Enable();
        shotgunButton.Enable();
        pistolButton.Enable();
        knifeButton.Enable();
        reloadButton.performed += Reload;
        rifleButton.performed += ChooseRifle;
        shotgunButton.performed += ChooseShotgun;
        pistolButton.performed += ChoosePistol;
        knifeButton.performed += ChooseKnife;
    }
    private void OnDisable()
    {
        fireButton.Disable();
        reloadButton.Disable();
        rifleButton.Disable();
        shotgunButton.Disable();
        pistolButton.Disable();
        knifeButton.Disable();
    }
    #endregion
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    private void Shoot(InputAction.CallbackContext context)
    {
        if(PlayerState.Instance.Aiming)
        {
            playerParticleSystem.Emit(1);
            RaycastHit hit;
            GameObject bullet = Instantiate(bulletPreFab,bulletTransform.position,bulletTransform.rotation);
            BulletScript bulletInstance = bullet.GetComponent<BulletScript>();
            GunManager.Instance.Shoot();
            if(Physics.Raycast(cameraTransform.position,cameraTransform.forward,out hit, Mathf.Infinity))
            {
                bulletInstance.target = hit.point;
                bulletInstance.hit = true; 
            }
            else
            {
                bulletInstance.target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
                bulletInstance.hit = true; 
            }
        } 
    }
    private void Reload(InputAction.CallbackContext context)
    {
        GunManager.Instance.Reload();
    }
    void ChooseRifle(InputAction.CallbackContext context)
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipRifle)
        {
            GunManager.Instance.WeaponEquipped = "Rifle";
            GunManager.Instance.SetWeaponChanges();
            Debug.Log(GunManager.Instance.reloadTime);
        }
    }
    void ChooseShotgun(InputAction.CallbackContext context)
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipShotgun)
        {
            GunManager.Instance.WeaponEquipped = "Shotgun";
            GunManager.Instance.SetWeaponChanges();
            Debug.Log(GunManager.Instance.reloadTime);
        }
    }
    void ChoosePistol(InputAction.CallbackContext context)
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipPistol)
        {
            GunManager.Instance.WeaponEquipped = "Pistol";
            GunManager.Instance.SetWeaponChanges();
            Debug.Log(GunManager.Instance.reloadTime);
        }
    }
    void ChooseKnife(InputAction.CallbackContext context)
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipKnife)
        {
            GunManager.Instance.WeaponEquipped = "Knife";
            GunManager.Instance.SetWeaponChanges();
            Debug.Log(GunManager.Instance.reloadTime);
        }
    }
}
