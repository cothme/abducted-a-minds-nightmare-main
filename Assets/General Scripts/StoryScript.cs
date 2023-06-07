using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScript : MonoBehaviour
{
    [TextArea] [SerializeField] string sentence;

    public string Sentence { get => sentence; set => sentence = value; }
}
