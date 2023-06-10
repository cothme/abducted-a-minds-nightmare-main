using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerShootingScript : MonoBehaviour
{
    [SerializeField] GameObject[] gunImages; 
    [SerializeField] GameObject bulletPreFab;
    [SerializeField] Transform bulletTransform;
    [SerializeField] GameObject aimCamera;
    [SerializeField] float bulletMissDistance = 25f;
    [SerializeField] TextMeshProUGUI magazineCountText;
    [SerializeField] TextMeshProUGUI totalBulletsCountText;
    [SerializeField] TextMeshProUGUI bulletsCountText;
    [SerializeField] TextMeshProUGUI statusIndicator;
    WeaponRecoil recoil;
    public bool isShooting = false;
    void Update()
    {
        totalBulletsCountText.text = GunManager.Instance.TotalBullets.ToString();
        bulletsCountText.text = GunManager.Instance.TotalBullets.ToString();
        // UpdateWeaponImageInUI();
        if(ControlsManager.Instance.IsRifleButtonDown)
        {
            ChooseRifle();
        }
        if(ControlsManager.Instance.IsShotgunButtonDown)
        {
            ChooseShotgun();
        }
        if(ControlsManager.Instance.IsPistolButtonDown)
        {
            ChoosePistol();
        }
        else if(ControlsManager.Instance.IsKnifeButtonDown)
        {
            ChooseKnife();
        }
        else if(ControlsManager.Instance.IsUnequipButtonDown)
        {
            GunManager.Instance.WeaponEquipped = null;
            Unequip();
        }
        if (Input.GetMouseButton(0) && GunManager.Instance.WeaponEquipped == "Rifle" && !isShooting)
        {
            StartCoroutine(ShootRifleCoroutine());
        }
        if(Input.GetMouseButtonDown(0) &&  GunManager.Instance.WeaponEquipped == "Knife" && !isShooting)
        {
            gameObject.GetComponent<Animator>().Play("Knife Attack 1");
        }
        if(Input.GetMouseButtonDown(0) && GunManager.Instance.WeaponEquipped == "Knife" && !isShooting)
        {
            KnifeAttack();
        }
        if(Input.GetMouseButtonDown(0) && GunManager.Instance.WeaponEquipped != "Rifle" && !isShooting && GunManager.Instance.WeaponEquipped != "Knife")
        {
            StartCoroutine(ShootCoroutine());
        }
        if(ControlsManager.Instance.IsReloadButtonDown)
        {
            Reload();
        }
    }
    void Start()
    {
        recoil = aimCamera.GetComponent<WeaponRecoil>();
    }
    private void ShootAssaultRifle()
    {
        if(PlayerState.Instance.Aiming && GunManager.Instance.BulletsLoaded != 0)
        {
            RaycastHit hit;
            GameObject bullet = Instantiate(bulletPreFab,bulletTransform.position,bulletTransform.rotation);
            BulletScript bulletInstance = bullet.GetComponent<BulletScript>();
            GunManager.Instance.shootInGunManager();
            if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit, Mathf.Infinity))
            {
                bulletInstance.target = hit.point;
                bulletInstance.hit = true; 
            }
            else
            {
                bulletInstance.target = Camera.main.transform.position + Camera.main.transform.forward * bulletMissDistance;
                bulletInstance.hit = true; 
            }
        } 
    }
    private void Shoot()
    {
        if(PlayerState.Instance.Aiming)
        {
            // recoil.StartShooting();
            gameObject.GetComponent<Animator>().Play("Fire HG");
            // RaycastHit hit;
            GameObject bullet = Instantiate(bulletPreFab,bulletTransform.position,gameObject.transform.rotation);
            // BulletScript bulletInstance = bullet.GetComponent<BulletScript>();
            GunManager.Instance.shootInGunManager();
            // if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit, Mathf.Infinity))
            // {
            //     bulletInstance.target = hit.point;
            //     bulletInstance.hit = true; 
            // }
            // else
            // {
            //     bulletInstance.target = Camera.main.transform.position + Camera.main.transform.forward * bulletMissDistance;
            //     bulletInstance.hit = true; 
            // } 
        }
    }
    private void KnifeAttack()
    {
        if(PlayerState.Instance.Aiming)
        {
            gameObject.GetComponent<Animator>().Play("Knife Attack 2");
        }
    }
    IEnumerator ShootRifleCoroutine()
    {
       isShooting = true;
        while (Mouse.current.leftButton.isPressed)
        {
            ShootAssaultRifle();
            yield return new WaitForSeconds(GunManager.Instance.AttackSpeed);
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
        yield return new WaitForSeconds(GunManager.Instance.AttackSpeed);       
        isShooting = false;
    }
    private void Reload()
    {
        if(!PlayerState.Instance.Reloading)
        {
            gameObject.GetComponent<Animator>().Play("Reload HG");  
            GunManager.Instance.reloadInGunManager();
        }
        else
        {
            return;
        }   
    }
    void ChooseRifle()
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipRifle && !PlayerState.Instance.Reloading)
        {
            GunManager.Instance.WeaponEquipped = "Rifle";
            GunManager.Instance.SetWeaponChanges();
            gunImages[0].SetActive(true);
            gunImages[1].SetActive(false);
            gunImages[2].SetActive(false);
            gunImages[3].SetActive(false);
        }
    }
    void ChooseShotgun()
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipShotgun && !PlayerState.Instance.Reloading)
        {
            GunManager.Instance.WeaponEquipped = "Shotgun";
            GunManager.Instance.SetWeaponChanges();
            gunImages[0].SetActive(false);
            gunImages[1].SetActive(true);
            gunImages[2].SetActive(false);
            gunImages[3].SetActive(false);
        }
    }
    void ChoosePistol()
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipPistol && !PlayerState.Instance.Reloading)
        {
            if(GunManager.Instance.WeaponEquipped != "Pistol")
            {
                gameObject.GetComponent<Animator>().Play("HG Equip");
            }
            GunManager.Instance.WeaponEquipped = "Pistol";
            GunManager.Instance.SetWeaponChanges();
            gunImages[0].SetActive(false);
            gunImages[1].SetActive(false);
            gunImages[2].SetActive(true);
            gunImages[3].SetActive(false);
        }
    }
    void ChooseKnife()
    {
        GunManager.Instance.CheckForWeapon();
        if(GunManager.Instance.CanEquipKnife && !PlayerState.Instance.Reloading)
        {
            gameObject.GetComponent<Animator>().Play("KF Equip");
            GunManager.Instance.WeaponEquipped = "Knife";
            GunManager.Instance.SetWeaponChanges();
            gunImages[0].SetActive(false);
            gunImages[1].SetActive(false);
            gunImages[2].SetActive(false);
            gunImages[3].SetActive(true);
        }
    }
    void Unequip()
    {
        GunManager.Instance.WeaponEquipped = null;
        GunManager.Instance.SetWeaponChanges();
        gunImages[0].SetActive(false);
        gunImages[1].SetActive(false);
        gunImages[2].SetActive(false);
        gunImages[3].SetActive(false);
    }
    void UpdateWeaponImageInUI()
    {
        if(!GunManager.Instance.CanEquipRifle)
        {
            gunImages[0].SetActive(false);
        }
        if(!GunManager.Instance.CanEquipShotgun)
        {
            gunImages[1].SetActive(false);
        }
        if(!GunManager.Instance.CanEquipPistol)
        {
            gunImages[2].SetActive(false);
        }
        if(!GunManager.Instance.CanEquipKnife)
        {
            gunImages[3].SetActive(false);
        }
    }
}
