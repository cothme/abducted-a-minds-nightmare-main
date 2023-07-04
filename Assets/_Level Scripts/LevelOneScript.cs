using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelOneScript : MonoBehaviour
{
    [SerializeField] GameObject roomFiveDoor;
    [SerializeField] PlayableDirector ending;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("dsdas");
        if(other.tag == "Player")
        {
            roomFiveDoor.tag = "Door";
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.None;
            ending.Play();
        }
    }
}
