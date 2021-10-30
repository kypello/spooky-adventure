using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public abstract class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    protected bool pointerOver;

    protected TMP_Text text;

    public Color highlightColor;
    public Color defaultColor;

    void Awake() {
        text = GetComponent<TMP_Text>();
    }

    void Update() {
        if (pointerOver) {
            text.color = highlightColor;

            if (Input.GetMouseButtonDown(0)) {
                Click();
            }
        }
        else {
            text.color = defaultColor;
        }
    }

    public abstract void Click();

    public void OnPointerEnter(PointerEventData eventData) {
        pointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        pointerOver = false;
    }
}
