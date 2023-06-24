using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ContinueScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        CheckSaveFile();
    }
    void CheckSaveFile()
    {  
        gameObject.GetComponent<Button>().interactable = File.Exists(Application.dataPath + "Abducted Save File");
    }
}
