using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : Interactable
{
    Renderer rend;
    Collider collider;

    public FlyTwitch flyA;
    public FlyTwitch flyB;
    public FlyTwitch flyC;

    public Transform flyTarget;

    void Start() {
        rend = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }

    public override IEnumerator Interact() {
        DisablePlayer();
        

        if (inventory.Contains("Scythe")) {
            StartCoroutine(playerLook.LookAt(flyTarget.position));
            rend.enabled = false;
            collider.enabled = false;

            flyA.transform.rotation = Quaternion.identity;
            flyB.transform.rotation = Quaternion.identity;
            flyC.transform.rotation = Quaternion.identity;

            flyA.flying = true;
            flyB.flying = true;
            flyC.flying = true;

            Vector3 flyDir = flyTarget.position - flyC.transform.position;

            while (flyA.transform.position.y < 10f) {
                flyA.transform.Translate(Vector3.up * Time.deltaTime * 4f);
                flyB.transform.Translate(Vector3.up * Time.deltaTime * 4f);

                if (Vector3.Distance(flyC.transform.position, flyTarget.position) > 0.1f) {
                    flyC.transform.Translate(flyDir * Time.deltaTime * 4f);
                }
                else {
                    flyC.flying = false;
                }
                yield return null;
            }

            flyA.gameObject.SetActive(false);
            flyB.gameObject.SetActive(false);
        }
        else {
            StartCoroutine(playerLook.LookAt(transform.position));
            yield return textBubble.Display("", "A number of flies are caught in this web.");
            yield return textBubble.Display("", "It's too strong to cut with your bare hands...");
        }


        EnablePlayer();
    }
}
