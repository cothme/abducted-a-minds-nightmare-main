using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoScript : MonoBehaviour
{
    [SerializeField] GameObject stalker;
    [SerializeField] public GameObject bossCage;
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
        if(PlayerState.Instance.LevelTwoCageUnlocked)
        {
            bossCage.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<DialogueScript>().showText("So the wall in to the boss is all an illusion?",5);
            stalker.SetActive(true);
        }
    }
}
