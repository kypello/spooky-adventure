using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    float volumeDelta = 0f;
    AudioSource audio;

    void Awake() {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            volumeDelta = 0.5f;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            volumeDelta = -0.5f;
        }
    }

    void Update() {
        audio.volume += volumeDelta * Time.deltaTime;
        if (audio.volume <= 0f || audio.volume >= 1f) {
            volumeDelta = 0f;
        }
    }
}
