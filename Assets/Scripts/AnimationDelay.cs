using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDelay : MonoBehaviour
{
    public float maxDelay;
    Animation anim;

    void Start() {
        anim = GetComponent<Animation>();
        StartCoroutine(Delay());
    }

    IEnumerator Delay() {
        yield return new WaitForSeconds(Random.Range(0f, maxDelay));
        anim.Play();
    }
}
