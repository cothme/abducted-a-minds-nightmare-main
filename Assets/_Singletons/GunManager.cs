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
    [SerializeField] TextMeshProUGUI magazineCountText;
    [SerializeField] TextMeshProUGUI totalBulletsCountText;
    [SerializeField] TextMeshProUGUI bulletsCountText;
    [SerializeField] TextMeshProUGUI statusIndicator;
    int bulletToMinus;
    private List<int> magazine;
    private int bulletsLoaded;
    private float reloadTime = 3.0f;
    private int totalBullets;
    public int BulletsLoaded { get { return bulletsLoaded; } }
    public int TotalBullets { get { return totalBullets; } }

    public List<int> Magazine { get => magazine; set => magazine = value; }

    private void Initialize()
    {
        magazine = new List<int> {  };
    }
    void Start()
    {
    }
    private void Update()
    {
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

        // Perform any additional actions before the reload completes

        while (remainingTime > 0)
        {
            statusIndicator.text = "Reloading: " + remainingTime.ToString("F1"); // Update the TextMeshProUGUI with remaining time
            yield return new WaitForSeconds(0.1f); // Wait for a short duration (adjust as needed)
            remainingTime -= 0.1f; // Decrease the remaining time
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
            yield break; // Exit the coroutine if the gun is already fully loaded
        }
        else
        {
            bulletToMinus = 30 - bulletsLoaded;
            bulletsLoaded += bulletToMinus;
            totalBullets -= bulletToMinus;
        } 
        foreach (int clip in magazine)
        {
            Debug.Log(clip);
        }
    }
}
