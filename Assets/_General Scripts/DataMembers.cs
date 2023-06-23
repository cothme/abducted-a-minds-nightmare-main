using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class DataMembers
{
    public string name;
    public float level;
    public float health;
    public float oxygen;
    public Quaternion rotation;
    public Vector3 position;
    public string weaponEquipped;
    public float gemsCollected;
    public float bulletsLoaded;
    public float totalBullets;
    public bool isSessionSaved;
    public bool aiming;
    public bool reloading;
    public bool running;
    public bool isPuzzleOneSolved;
    public bool levelOneDoorUnlocked;
    public bool levelOneCageUnlocked;
    public bool canEquipPistol;
    public bool canEquipShotgun;
    public bool canEquipKnife;
    public bool canEquipRifle;
    public bool paused;
    public List<int> itemList;
    public float playerMaxHealth;
}
