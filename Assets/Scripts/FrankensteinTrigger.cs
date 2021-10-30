using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankensteinTrigger : MonoBehaviour
{
    public Frankenstein frankenstein;
    public Animation door;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            StartCoroutine(frankenstein.Interact());
        }
        else if (collider.gameObject.tag == "Partygoer") {
            StartCoroutine(OpenClose());
        }
    }

    IEnumerator OpenClose() {
        door.Play("DoorOpen");
        yield return new WaitForSeconds(1f);

        if (!frankenstein.doorPermanentlyOpen) {
            door.Play("DoorClose");
        }
    }
}
