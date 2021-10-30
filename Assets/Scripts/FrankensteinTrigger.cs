using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankensteinTrigger : MonoBehaviour
{
    public Frankenstein frankenstein;
    public Animation door;
    public CharacterController player;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            StartCoroutine(WaitForGrounded());
        }
        else if (collider.gameObject.tag == "Partygoer") {
            StartCoroutine(OpenClose());
        }
    }

    IEnumerator OpenClose() {
        door.Play("DoorOpen");
        yield return new WaitForSeconds(1.5f);

        if (!frankenstein.doorPermanentlyOpen) {
            door.Play("DoorClose");
        }
    }

    IEnumerator WaitForGrounded() {
        while (!player.isGrounded) {
            yield return null;
        }
        StartCoroutine(frankenstein.Interact());
    }
}
