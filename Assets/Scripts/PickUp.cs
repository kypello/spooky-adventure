using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public float throwStrength;
    public float pickupRange;
    public Sprite crosshairDefault;
    public Sprite crosshairInteract;

    public Image crosshair;

    public Transform pickupPoint;

    public AudioSource throwSound;
    public AudioSource pickupSound;

    public bool holdingItem;

    GameObject holding;

    void Update() {
        if (holding == null) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, 1<<6)) {
                crosshair.sprite = crosshairInteract;
                if (Input.GetMouseButtonDown(0)) {
                    PickupObject(hit.collider.gameObject);
                }
            }
            else {
                crosshair.sprite = crosshairDefault;
            }
        }
        else {
            if (Input.GetMouseButtonDown(0)) {
                ThrowObject();
            }
        }
    }

    void PickupObject(GameObject pickup) {
        pickup.GetComponent<Collider>().enabled = false;
        pickup.GetComponent<Rigidbody>().isKinematic = true;
        pickup.transform.parent = pickupPoint;
        pickup.transform.localPosition = Vector3.zero;
        holding = pickup;
        holdingItem = true;

        pickupSound.Play();
    }

    void ThrowObject() {
        holding.GetComponent<Collider>().enabled = true;
        holding.GetComponent<Rigidbody>().isKinematic = false;
        holding.GetComponent<Rigidbody>().velocity = transform.forward * throwStrength;
        holding.transform.parent = null;
        holding = null;
        holdingItem = false;

        throwSound.Play();
    }
}
