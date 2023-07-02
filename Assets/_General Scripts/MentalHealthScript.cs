using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MentalHealthScript : MonoBehaviour
{
    PlayableDirector mentalHealthTip;
    private void Start()
    {
        mentalHealthTip = GetComponent<PlayableDirector>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            mentalHealthTip.Play();
            Destroy(gameObject.GetComponent<BoxCollider>());
        }
    }
}
