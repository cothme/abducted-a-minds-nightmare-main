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
        gameObject.GetComponent<Button>().interactable = File.Exists("D:\\CAPSTONE_1_PROJECT_FILES\\abducted-a-minds-nightmare-main\\Abducted Save File");
    }
}
