using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderJumpscare : MonoBehaviour
{
    public Animation jumpscareA;
    public Animation wiggleA;

    public Animation jumpscareB;
    public Animation wiggleB;

    public Transform player;

    void OnTriggerEnter(Collider other) {
        Animation jumpscare;
        Animation wiggle;

        if (player.transform.forward.x > 0) {
            jumpscareA.Play();
        }
        else {
            jumpscareB.Play();
        }
    }
}
