using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocksScript : MonoBehaviour
{
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "level 1")
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
        else if(SceneManager.GetActiveScene().name == "level 2")
        {
            if(PlayerState.Instance.LevelTwoDoorUnlocked)
            {
                gameObject.tag = "Door";
            }
            else
            {
                gameObject.tag = "Untagged";
            }  
        }        
    }
}
