using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTextPressOffset : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform textTransform;
    public float offsetY = -5f;

    private Vector2 startPos;

    void Start()
    {
        startPos = textTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        textTransform.anchoredPosition = startPos + new Vector2(0, offsetY);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        textTransform.anchoredPosition = startPos;
    }
}