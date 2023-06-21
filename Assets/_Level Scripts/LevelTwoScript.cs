using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoScript : MonoBehaviour
{
    [SerializeField] AudioSource soundToPlay;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            soundToPlay.Play();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
