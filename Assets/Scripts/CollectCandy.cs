using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectCandy : MonoBehaviour
{
    public ParticleSystem candyCollectParticles;
    public int candy = 0;
    public TMP_Text candyCount;
    public Animation candyCountAnim;

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
            candyCountAnim["CandyCountBounce"].time = 0f;
            candyCountAnim.Play();
            candy++;

            candyCount.text = "" + candy;
        }
    }
}
