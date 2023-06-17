using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StoryScript : MonoBehaviour
{
    [TextArea] [SerializeField] string sentence;
    [SerializeField] PlayableDirector runnersIntro;
    [SerializeField] public Sprite UVsprite;

    public string Sentence { get => sentence; set => sentence = value; }

    void OnTriggerEnter(Collider col)
    {
        if(gameObject.tag == "Prompt")
        {
            if(col.tag == "Player")
            {
                runnersIntro.Play(); 
            }
        }
    }
}
