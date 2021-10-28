using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    bool playerInRange = false;
    public AudioSource jumpscareSound;
    public Transform player;
    public float range;
    public Animation anim;

    void Update() {
        if (Vector3.Distance(player.position, transform.position) < range) {
            if (!playerInRange) {
                jumpscareSound.Play();
                playerInRange = true;
            }

            if (!anim.isPlaying) {
                anim.Play();
            }

            Vector3 playerZero = new Vector3(player.position.x, 0f, player.position.z);
            Vector3 playerDir = (playerZero - transform.position).normalized;
            if (Vector3.Dot(transform.forward, playerDir) < 0.999f) {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, playerDir, Mathf.PI * 2f * Time.deltaTime, 0f));
            }
        }
        else {
            playerInRange = false;
        }
    }
}
