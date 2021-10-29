using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankensteinTrigger : MonoBehaviour
{
    public Frankenstein frankenstein;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            StartCoroutine(frankenstein.Interact());
        }
    }
}
