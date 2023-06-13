using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerShootingScript : MonoBehaviour
{
    [SerializeField] GameObject rifle;
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject knife;
    [SerializeField] GameObject healthkit;
    [SerializeField] GameObject oxygenkit;
    [SerializeField] GameObject mask;
    [SerializeField] GameObject[] gunImages; 
    [SerializeField] GameObject bulletPreFab;
    [SerializeField] Transform bulletTransform;
    [SerializeField] GameObject aimCamera;
    [SerializeField] float bulletMissDistance = 25f;
    [SerializeField] TextMeshProUGUI magazineCountText;
    [SerializeField] TextMeshProUGUI totalBulletsCountText;
    [SerializeField] TextMeshProUGUI bulletsCountText;
    WeaponRecoil recoil;
    bool maskEquipped = false;
    public bool isShooting = false;
    void Update()
    {
        totalBulletsCountText.text = GunManager.Instance.TotalBullets.ToString();
        bulletsCountText.text = GunManager.Instance.BulletsLoaded.ToString();
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
        else if(ControlsManager.Instance.IsMaskEquipButtonDown)
        {
            EquipMask();
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
        if(Input.GetMouseButtonDown(0) &&  GunManager.Instance.WeaponEquipped == "Knife" && !isShooting && !PlayerState.Instance.Aiming)
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
            gameObject.GetComponent<Animator>().Play("Rif Fire");
            GameObject bullet = Instantiate(bulletPreFab,bulletTransform.position,gameObject.transform.rotation);
            GunManager.Instance.shootInGunManager();
        } 
    }
    private void Shoot()
    {
        if(PlayerState.Instance.Aiming && GunManager.Instance.BulletsLoaded != 0)
        {
            gameObject.GetComponent<Animator>().Play("Fire HG");
            GameObject bullet = Instantiate(bulletPreFab,bulletTransform.position,gameObject.transform.rotation);
            GunManager.Instance.shootInGunManager();
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
            if(GunManager.Instance.WeaponEquipped == "Rifle")
            {
                gameObject.GetComponent<Animator>().Play("Rif Reload");  
                GunManager.Instance.reloadInGunManager();
            }
            else if(GunManager.Instance.WeaponEquipped == "Shotgun")
            {
                gameObject.GetComponent<Animator>().Play("SG Reload");  
                GunManager.Instance.reloadInGunManager();
            }
            else if(GunManager.Instance.WeaponEquipped == "Pistol")
            {
                gameObject.GetComponent<Animator>().Play("HG Reload"); 
                GunManager.Instance.reloadInGunManager();             
            }
            else if(GunManager.Instance.WeaponEquipped == "Knife")
            {
                return; 
            }
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
            if(GunManager.Instance.WeaponEquipped != "Rifle")
            {
                gameObject.GetComponent<Animator>().Play("Rif Equip");
                rifle.SetActive(true);
            }
            GunManager.Instance.WeaponEquipped = "Rifle";
            GunManager.Instance.SetWeaponChanges();
            rifle.SetActive(true);
            shotgun.SetActive(false);
            pistol.SetActive(false);
            knife.SetActive(false);
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
            if(GunManager.Instance.WeaponEquipped != "Shotgun")
            {
                gameObject.GetComponent<Animator>().Play("SG Equip");
            }
            GunManager.Instance.WeaponEquipped = "Shotgun";
            GunManager.Instance.SetWeaponChanges();
            rifle.SetActive(false);
            shotgun.SetActive(true);
            pistol.SetActive(false);
            knife.SetActive(false);
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
            rifle.SetActive(false);
            shotgun.SetActive(false);
            pistol.SetActive(true);
            knife.SetActive(false);
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
            rifle.SetActive(false);
            shotgun.SetActive(false);
            pistol.SetActive(false);
            knife.SetActive(true);
            gunImages[0].SetActive(false);
            gunImages[1].SetActive(false);
            gunImages[2].SetActive(false);
            gunImages[3].SetActive(true);
        }
    }
    
    private void EquipMask()
    {
        if(GunManager.Instance.CanEquipMask && maskEquipped == false)
        {
            maskEquipped = !maskEquipped;
            gameObject.GetComponent<Animator>().Play("Equip Space Mask");
            mask.SetActive(true);
        }
        else if(maskEquipped)
        {
            maskEquipped = !maskEquipped;
            mask.SetActive(false);
        }
        else
        {
            mask.SetActive(false);  
        }
    }
    void Unequip()
    {
        GunManager.Instance.WeaponEquipped = null;
        GunManager.Instance.SetWeaponChanges();
        rifle.SetActive(false);
        shotgun.SetActive(false);
        pistol.SetActive(false);
        knife.SetActive(false);
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
