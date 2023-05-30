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
    float shootTimer = 0f;
    bool isShooting = false;
    void Update()
    {
        if(ControlsManager.Instance.IsRifleButtonDown)
        {
            ChooseRifle();
        }
        else if(ControlsManager.Instance.IsShotgunButtonDown)
        {
            ChooseShotgun();
        }
        else if(ControlsManager.Instance.IsPistolButtonDown)
        {
            ChoosePistol();
        }
        else if(ControlsManager.Instance.IsKnifeButtonDown)
        {
            ChooseKnife();
        }
        if (Input.GetMouseButton(0) && GunManager.Instance.WeaponEquipped == "Rifle" && !isShooting)
        {
            StartCoroutine(ShootRifleCoroutine());
        }
        if(Input.GetMouseButtonDown(0) && GunManager.Instance.WeaponEquipped != "Rifle" && !isShooting)
        {
            Debug.Log("ssss");
            StartCoroutine(ShootCoroutine());
        }
        if(ControlsManager.Instance.IsReloadButtonDown)
        {
            Reload();
        }
    }
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
            GunManager.Instance.shootInGunManager();
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
    private void Shoot()
    {
        if(PlayerState.Instance.Aiming && GunManager.Instance.BulletsLoaded != 0)
        {
            playerParticleSystem.Emit(1);
            RaycastHit hit;
            GameObject bullet = Instantiate(bulletPreFab,bulletTransform.position,bulletTransform.rotation);
            BulletScript bulletInstance = bullet.GetComponent<BulletScript>();
            GunManager.Instance.shootInGunManager();
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
    IEnumerator ShootCoroutine()
    {   
        isShooting = false;
        if(!isShooting)
        {
            isShooting = true;
            Shoot();
        }
        yield return new WaitForSeconds(GunManager.Instance.attackSpeed);        
        isShooting = false;
    }
    private void Reload()
    {
        GunManager.Instance.reloadInGunManager();
    }
    void ChooseRifle()
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipRifle && !PlayerState.Instance.Reloading)
        {
            GunManager.Instance.WeaponEquipped = "Rifle";
            GunManager.Instance.SetWeaponChanges();
        }
    }
    void ChooseShotgun()
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipShotgun && !PlayerState.Instance.Reloading)
        {
            GunManager.Instance.WeaponEquipped = "Shotgun";
            GunManager.Instance.SetWeaponChanges();
        }
    }
    void ChoosePistol()
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipPistol && !PlayerState.Instance.Reloading)
        {
            GunManager.Instance.WeaponEquipped = "Pistol";
            GunManager.Instance.SetWeaponChanges();
        }
    }
    void ChooseKnife()
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipKnife && !PlayerState.Instance.Reloading)
        {
            GunManager.Instance.WeaponEquipped = "Knife";
            GunManager.Instance.SetWeaponChanges();
        }
    }
}
