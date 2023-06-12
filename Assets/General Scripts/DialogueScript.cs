using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] [TextAreaAttribute] string subtitle;
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] TextMeshProUGUI canvasText;
    [SerializeField] float deletionTime;
    bool shownSubtitle = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("dsadsad");
        if(other.tag == "Player")
        {
            StartCoroutine(ShowSubtitle());
            shownSubtitle = true;
        }
        else
        {
            return;
        }
    }  
    IEnumerator ShowSubtitle()
    {
        canvasText.text = subtitle;
        dialogueCanvas.enabled = true;
        yield return new WaitForSeconds(deletionTime);
        dialogueCanvas.enabled = false;
    }
}
