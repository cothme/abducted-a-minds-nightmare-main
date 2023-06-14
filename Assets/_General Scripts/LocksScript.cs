using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocksScript : MonoBehaviour
{
    void Update()
    {
        if(PlayerState.Instance.LevelOneDoorUnlocked)
        {
            gameObject.tag = "Door";
        }
        else
        {
             gameObject.tag = "Untagged";
        }     
    }
}
