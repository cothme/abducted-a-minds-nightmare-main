using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneScript : MonoBehaviour
{
    [SerializeField] GameObject roomFiveDoor;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("dsdas");
        if(other.tag == "Player")
        {
            roomFiveDoor.tag = "Door";
        }
    }
}
