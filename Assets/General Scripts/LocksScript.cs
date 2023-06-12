using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocksScript : MonoBehaviour
{
    void Update()
    {
        if(PlayerState.Instance.LevelOneDoorUnlocked)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }     
    }
}
