using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoScript : MonoBehaviour
{
    [SerializeField] public GameObject bossCage;
    [SerializeField] public GameObject doorPuzzle;
    bool doorUnlocked = true;
    [SerializeField] GameObject stalker;
    [SerializeField] AudioSource soundToPlay;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            soundToPlay.Play();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
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
