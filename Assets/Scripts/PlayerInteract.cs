using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public RectTransform crosshair;
    public Image crosshairImage;
    public float interactRange = 4f;
    public bool control = true;

    void Update() {
        crosshairImage.enabled = control;
        
        RaycastHit hit;

        if (control && Physics.Raycast(transform.position, transform.forward, out hit, interactRange, 1<<6)) {
            crosshair.sizeDelta = Vector2.one * 12;

            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("Interact");
                StartCoroutine(hit.collider.GetComponent<Interactable>().Interact());
            }
        }
        else {
            crosshair.sizeDelta = Vector2.one * 4;
        }
    }
}
