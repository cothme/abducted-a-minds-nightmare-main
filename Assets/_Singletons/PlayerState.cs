using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;
    private float movingX;
    private float movingZ;
    private bool aiming;
    private bool reloading;
    private bool running;
    private bool levelOneDoorUnlocked = false;
    private bool levelTwoDoorUnlocked = false;
    private bool levelOneCageUnlocked = false;
    private bool levelTwoCageUnlocked = false;
    private bool levelThreeCageUnlocked = false;
    private bool isPuzzleOneSolved = false;
    private bool isPuzzleTwoSolved = false;
    private bool isPuzzleThreeSolved = false;
    private bool levelOneBossDefeated = false;
    private bool levelTwoBossDefeated = false;
    private int levelThreeBossDefeated = 0;
    private bool walking;
    private bool isReading = false;
    private bool isDead = false;
    private bool isUVOn = false;
    private bool canStoreItem = true;
    private bool levelFourBossDefeated = false;
    private bool runnersHit = false;
    private bool brutesHit = false;
    public bool Aiming { get => aiming; set => aiming = value; }
    public bool Reloading { get => reloading; set => reloading = value; }
    public bool Running { get => running; set => running = value; }
    public float MovingX { get => movingX; set => movingX = value; }
    public float MovingZ { get => movingZ; set => movingZ = value; }
    public bool IsPuzzleOneSolved { get => isPuzzleOneSolved; set => isPuzzleOneSolved = value; }
    public bool LevelOneDoorUnlocked { get => levelOneDoorUnlocked; set => levelOneDoorUnlocked = value; }
    public bool LevelOneCageUnlocked { get => levelOneCageUnlocked; set => levelOneCageUnlocked = value; }
    public bool Walking { get => walking; set => walking = value; }
    public bool LevelOneBossDefeated { get => levelOneBossDefeated; set => levelOneBossDefeated = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public bool LevelTwoDoorUnlocked { get => levelTwoDoorUnlocked; set => levelTwoDoorUnlocked = value; }
    public bool IsPuzzleTwoSolved { get => isPuzzleTwoSolved; set => isPuzzleTwoSolved = value; }
    public bool LevelTwoCageUnlocked { get => levelTwoCageUnlocked; set => levelTwoCageUnlocked = value; }
    public bool IsReading { get => isReading; set => isReading = value; }
    public bool IsUVOn { get => isUVOn; set => isUVOn = value; }
    public bool LevelTwoBossDefeated { get => levelTwoBossDefeated; set => levelTwoBossDefeated = value; }
    public bool IsPuzzleThreeSolved { get => isPuzzleThreeSolved; set => isPuzzleThreeSolved = value; }
    public bool LevelThreeCageUnlocked { get => levelThreeCageUnlocked; set => levelThreeCageUnlocked = value; }
    public int LevelThreeBossDefeated { get => levelThreeBossDefeated; set => levelThreeBossDefeated = value; }
    public bool CanStoreItem { get => canStoreItem; set => canStoreItem = value; }
    public bool LevelFourBossDefeated { get => levelFourBossDefeated; set => levelFourBossDefeated = value; }
    public bool RunnersHit { get => runnersHit; set => runnersHit = value; }
    public bool BrutesHit { get => brutesHit; set => brutesHit = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
