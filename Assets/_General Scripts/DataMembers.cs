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
    public List<int> itemList;
}
