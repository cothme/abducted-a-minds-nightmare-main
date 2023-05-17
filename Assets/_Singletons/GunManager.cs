using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GunManager : MonoBehaviour
{
    private static GunManager instance;
    public static GunManager Instance { get { return instance; } }
    [SerializeField] List<WeaponData> weapons;
    [SerializeField] TextMeshProUGUI magazineCountText;
    [SerializeField] TextMeshProUGUI totalBulletsCountText;
    [SerializeField] TextMeshProUGUI bulletsCountText;
    [SerializeField] TextMeshProUGUI statusIndicator;
    [SerializeField] TextMeshProUGUI weaponIndicator;
    private string weaponEquipped;
    private int bulletsLoaded;
    private int totalBullets;
    public float reloadTime;
    private bool canEquipPistol, canEquipRifle, canEquipShotgun,canEquipKnife;
    int bulletToMinus;
    private List<int> magazine;
    public List<int> Magazine { get => magazine; set => magazine = value; }
    public bool CanEquipPistol { get => canEquipPistol; set => canEquipPistol = value; }
    public bool CanEquipRifle { get => canEquipRifle; set => canEquipRifle = value; }
    public bool CanEquipShotgun { get => canEquipShotgun; set => canEquipShotgun = value; }
    public bool CanEquipKnife { get => canEquipKnife; set => canEquipKnife = value; }
    public string WeaponEquipped { get => weaponEquipped; set => weaponEquipped = value; }
    private void Initialize()
    {
        magazine = new List<int> {  };
    }
    private void Update()
    {
        weaponIndicator.text = WeaponEquipped;
        totalBulletsCountText.text = totalBullets.ToString();
        bulletsCountText.text = bulletsLoaded.ToString();
    }
    public void Shoot()
    {
        if(bulletsLoaded <= 0)
        {
            return;
        }
        else
        {
            bulletsLoaded -= 1;
            magazine[0] -= 1;
        }
    }
    public void Reload()
    {
        if(totalBullets <= 0 || magazine.Count == 0)
        {
            return;
        }
        StartCoroutine(ReloadCoroutine());
    }
    public void UpdateBullets()
    {   
        totalBullets = magazine.Sum() - bulletsLoaded;
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Initialize();
    }
    private IEnumerator ReloadCoroutine()
    {
        float remainingTime = reloadTime;
        while (remainingTime > 0)
        {
            statusIndicator.text = "Reloading: " + reloadTime.ToString("F1");
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f; 
            // foreach (int clip in magazine)
            // {
            //     Debug.Log(clip);
            // }
        }   
        if (magazine[0] <= 0)
            {
                magazine.RemoveAt(0);
            }
            if (bulletsLoaded == 0)
            {
                bulletToMinus = magazine[0];
                totalBullets -= bulletToMinus;
                bulletsLoaded += bulletToMinus;
            }
            else if (bulletsLoaded >= 30)
            {
                yield break;
            }
            else
            {
                bulletToMinus = 30 - bulletsLoaded;
                bulletsLoaded += bulletToMinus;
                totalBullets -= bulletToMinus;
            }
    }
    public void CheckForWeapon()
    {
        foreach (int item in ItemList.Instance.Itemlist)
        {
            switch (item)
            {
                case 1:
                    CanEquipRifle = true;
                    break;
                case 5:
                    CanEquipPistol = true;
                    break;
                case 6:
                    CanEquipShotgun = true;
                    break;
                case 2:
                    CanEquipKnife = true;
                    break;
                default:
                    break;
            }
        }
    }
   public void SetWeaponChanges()
   {
        switch (weaponEquipped)
        {
            case "Rifle":
                reloadTime = weapons[0].reloadSpeed;
                break;
            case "Shotgun":
                reloadTime = weapons[1].reloadSpeed;
                break;
            case "Pistol":
                reloadTime = weapons[2].reloadSpeed;
                break;
            case "Knife":
                reloadTime = weapons[3].reloadSpeed;
                break;
            default:
                break;
        }
   } 
}
