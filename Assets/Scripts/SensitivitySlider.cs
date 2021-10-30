using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool pointerOver;
    bool dragging;

    public Image image;

    public Color highlightColor;
    public Color defaultColor;

    void Update() {
        if (!dragging) {
            if (pointerOver) {
                image.color = highlightColor;

                if (Input.GetMouseButtonDown(0)) {
                    dragging = true;
                }
            }
            else {
                image.color = defaultColor;
            }
        }
        else {
            Debug.Log(Input.mousePosition.x / Screen.width);

            if (!Input.GetMouseButton(0)) {
                dragging = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        pointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        pointerOver = false;
    }
}
