using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTwitch : MonoBehaviour
{
    Animation anim;
    float time = 0f;

    public bool flying = false;

    void Awake() {
        anim = GetComponent<Animation>();
    }

    void Update() {
        time -= Time.deltaTime;
        if ((flying && !anim.isPlaying) || time <= 0f) {
            anim.Play();
            time = Random.Range(1f, 5f);
        }
    }
}
