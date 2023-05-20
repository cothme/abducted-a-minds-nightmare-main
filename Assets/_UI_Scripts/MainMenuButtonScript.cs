using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class MainMenuButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    private Vector3 originalPosition,targetPosition;
    void Start()
    {
        try
        {
            originalPosition = text.transform.localPosition;
            targetPosition = new Vector3(100,0,0);
        }
        catch(NullReferenceException)
        {
            return;
        } 
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        try
        {
            text.transform.localPosition = targetPosition;
        }
        catch(NullReferenceException)
        {
            return;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        try
        {
            text.transform.localPosition = originalPosition;
        }
        catch(NullReferenceException)
        {
            return;
        }
    }
    IEnumerator ButtonAnimation(Vector3 targetPosition)
    {
        yield break;
    }
}
