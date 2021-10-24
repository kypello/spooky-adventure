using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTwitch : MonoBehaviour
{
    Animation anim;
    float time = 0f;

    void Awake() {
        anim = GetComponent<Animation>();
    }

    void Update() {
        time -= Time.deltaTime;
        if (time <= 0f) {
            anim.Play();
            time = Random.Range(2f, 8f);
        }
    }
}
