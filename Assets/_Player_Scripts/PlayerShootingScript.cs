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
    float shootTimer = 0f;
    bool isShooting = false;
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
    void Update()
    {
        if (Mouse.current.leftButton.isPressed && GunManager.Instance.WeaponEquipped == "Rifle" && !isShooting)
        {
            StartCoroutine(ShootRifleCoroutine()); // Start shooting coroutine
        }
        else
        {
            return;
        }
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
    private void ShootAssaultRifle()
    {
        if(PlayerState.Instance.Aiming && GunManager.Instance.BulletsLoaded != 0)
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
    private void Shoot(InputAction.CallbackContext context)
    {
        if(GunManager.Instance.WeaponEquipped == "Rifle")
        {
            return;
        }
        else
        {
            if(PlayerState.Instance.Aiming && GunManager.Instance.BulletsLoaded != 0)
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
    }
    IEnumerator ShootRifleCoroutine()
    {
       isShooting = true;

        while (Mouse.current.leftButton.isPressed)
        {
            ShootAssaultRifle();
            yield return new WaitForSeconds(GunManager.Instance.attackSpeed);
        }
        isShooting = false;
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
        }
    }
    void ChooseShotgun(InputAction.CallbackContext context)
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipShotgun)
        {
            GunManager.Instance.WeaponEquipped = "Shotgun";
            GunManager.Instance.SetWeaponChanges();
        }
    }
    void ChoosePistol(InputAction.CallbackContext context)
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipPistol)
        {
            GunManager.Instance.WeaponEquipped = "Pistol";
            GunManager.Instance.SetWeaponChanges();
        }
    }
    void ChooseKnife(InputAction.CallbackContext context)
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipKnife)
        {
            GunManager.Instance.WeaponEquipped = "Knife";
            GunManager.Instance.SetWeaponChanges();
        }
    }
}
