using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class MainMenuButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private RectTransform rectTransform;
    private TextMeshProUGUI textMesh;
    private Outline textOutline;
    private bool isHovering = false;
    [SerializeField] Color hoverColor;
    [SerializeField] Color exitHoverColor;

    void Start()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        textOutline = textMesh.GetComponent<Outline>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        StartCoroutine(ButtonMoveAnimation(10f, hoverColor, true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        StartCoroutine(ButtonMoveAnimation(-10f, Color.white, false));
    }

    IEnumerator ButtonMoveAnimation(float moveAmount, Color color, bool enableOutline)
    {
        float duration = 0.2f;
        float timer = 0f;
        Vector2 startPosition = rectTransform.anchoredPosition;
        Color startColor = textMesh.color;
        bool startOutlineState = textOutline.enabled;

        while (timer < duration)
        {
            float t = timer / duration;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, startPosition + new Vector2(moveAmount, 0f), t);
            textMesh.color = Color.Lerp(startColor, color, t);
            textOutline.enabled = enableOutline;
            timer += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = startPosition + new Vector2(moveAmount, 0f);
        textMesh.color = color;
        textOutline.enabled = enableOutline;

        if (!isHovering)
        {
            timer = 0f;
            while (timer < duration)
            {
                float t = timer / duration;
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition + new Vector2(moveAmount, 0f), startPosition, t);
                textMesh.color = Color.Lerp(color, startColor, t);
                timer += Time.deltaTime;
                yield return null;
            }

            rectTransform.anchoredPosition = startPosition;
            textMesh.color = startColor;
        }
    }
}