using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    private static PlayerData instance;
    Vector3 playerPosition;
    float playerHealth = 50f;
    float playerOxygen = 100f;
    float gemsCollected = 0f;
    public static PlayerData Instance { get { return instance; } }
    public float PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public float PlayerOxygen { get => playerOxygen; set => playerOxygen = value; }
    public Vector3 PlayerPosition { get => playerPosition; set => playerPosition = value; }
    public float GemsCollected { get => gemsCollected; set => gemsCollected = value; }

    void Update()
    {
        int level = Level();
    }
    public int Level()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "Level 1":
            return 1;
            case "Level 2":
            return 2;
            case "Level 3":
            return 3;
            default:
            return 0;
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
