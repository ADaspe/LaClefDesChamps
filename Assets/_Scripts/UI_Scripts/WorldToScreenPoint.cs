using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldToScreenPoint : MonoBehaviour
{
    public Transform target;
    public RectTransform CanvasRect;
    public RectTransform UIelement;
    public float yoffset;

    // Update is called once per frame
    void Update()
    {
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(target.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)) + yoffset);

        UIelement.anchoredPosition = WorldObject_ScreenPosition;
    }

}
