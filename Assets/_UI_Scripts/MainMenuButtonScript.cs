using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

public class MainMenuButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image highlightImage;
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
            highlightImage.enabled = true;
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
            highlightImage.enabled = false;
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
    public void LoadData()
    {
        SceneManager.LoadScene("MockScene");
    }
    public void QuitClicked()
    {
        Debug.Log(PlayerData.Instance.PlayerPosition);
    }
}
