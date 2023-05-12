using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static ItemList Instance;
    public List<int> itemList;

    public void AddItem(string name)
    {
        switch(name)
        {
            case "Ammo":
            itemList.Add(0);
            break;
            case "Assault Rifle":
            itemList.Add(1);
            break;
            case "Knife":
            itemList.Add(2);
            break;
            case "Mask":
            itemList.Add(3);
            break;
            case "Oxygen Kit":
            itemList.Add(4);
            break;
            case "Pistol":
            itemList.Add(5);
            break;
        }
    }    
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
