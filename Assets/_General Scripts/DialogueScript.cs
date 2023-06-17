using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] [TextAreaAttribute] public string subtitle;
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] TextMeshProUGUI canvasText;
    [SerializeField] public float deletionTime;
    bool shownSubtitle = false;

    void OnTriggerEnter(Collider other)
    {
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
    public IEnumerator ShowSubtitle()
    {
        canvasText.text = subtitle;
        dialogueCanvas.enabled = true;
        yield return new WaitForSeconds(deletionTime);
        dialogueCanvas.enabled = false;
    }
    public IEnumerator ShowSubtitle2(string text, float deletetionTime)
    {
        canvasText.text = text;
        dialogueCanvas.enabled = true;
        yield return new WaitForSeconds(deletetionTime);
        dialogueCanvas.enabled = false;
    }
    public void showText(string text, float deletetionTime)
    {
        subtitle = text;
        this.deletionTime = deletetionTime;
        StartCoroutine(ShowSubtitle());
    }
}
