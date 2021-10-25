using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCandy : MonoBehaviour
{
    public ParticleSystem candyCollectParticles;
    public int candy = 0;

    AudioSource collectSound;

    void Awake() {
        collectSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Candy") {
            candyCollectParticles.transform.position = other.transform.position;
            candyCollectParticles.Play();
            other.gameObject.SetActive(false);
            collectSound.Play();
            candy++;
        }
    }
}
