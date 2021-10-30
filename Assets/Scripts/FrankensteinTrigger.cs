using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankensteinTrigger : MonoBehaviour
{
    public Frankenstein frankenstein;
    public Animation door;
    public CharacterController player;

    public AudioSource doorOpen;
    public AudioSource doorClose;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player" && !frankenstein.doorPermanentlyOpen) {
            StartCoroutine(WaitForGrounded());
        }
        else if (collider.gameObject.tag == "Partygoer") {
            StartCoroutine(OpenClose(collider));
        }
    }

    IEnumerator OpenClose(Collider collider) {
        collider.enabled = false;

        if (!frankenstein.doorPermanentlyOpen) {
            door.Play("DoorOpen");
            doorOpen.Play();
        }
        
        yield return new WaitForSeconds(1.5f);

        if (!frankenstein.doorPermanentlyOpen) {
            door.Play("DoorClose");
            yield return new WaitForSeconds(0.75f);
            doorClose.Play();
        }

        yield return new WaitForSeconds(4f);

        collider.enabled = true;
    }

    IEnumerator WaitForGrounded() {
        while (!player.isGrounded) {
            yield return null;
        }
        StartCoroutine(frankenstein.Interact());
    }
}
