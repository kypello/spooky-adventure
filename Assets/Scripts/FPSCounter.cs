using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    TMP_Text text;

    void Awake() {
        text = GetComponent<TMP_Text>();
    }

    void Update() {
        text.text = "FPS: " + Mathf.FloorToInt(1f / Time.deltaTime);
    }
}
