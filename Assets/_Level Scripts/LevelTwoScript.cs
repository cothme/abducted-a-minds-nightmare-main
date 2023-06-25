using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoScript : MonoBehaviour
{
    [SerializeField] GameObject stalker;
    [SerializeField] public GameObject bossCage;
    [SerializeField] public GameObject doorPuzzle;
    bool doorUnlocked = true;
    private void Update()
    {
        if(PlayerState.Instance.LevelTwoCageUnlocked && doorUnlocked == true)
        {
            bossCage.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<DialogueScript>().showText("So the wall in to the boss is all an illusion?",3);
            stalker.SetActive(true);
            doorUnlocked = false;
        }
    }
}
