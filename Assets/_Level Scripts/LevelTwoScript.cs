using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoScript : MonoBehaviour
{
    [SerializeField] public GameObject bossCage;
    [SerializeField] public GameObject doorPuzzle;
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
            Destroy(bossCage);
        }
    }
}