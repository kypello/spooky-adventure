using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : Interactable
{
    public GameObject pumpkin;

    bool pumpkinPlaced = false;

    public GameObject crowA;
    public GameObject crowB;
    public GameObject crowKeyProp;
    public GameObject key;

    public override IEnumerator Interact() {
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 3f));

        if (pumpkinPlaced) {
            yield return textBubble.Display("Scarecrow", "Please find me something to wear!!");
        }
        else if (inventory.Contains("PumpkinHead")) {
            inventory.RemoveItem("PumpkinHead");
            pumpkin.SetActive(true);
            pumpkinPlaced = true;

            yield return textBubble.Display("Scarecrow", "Ahhhhh, that's so much better, thanks!!");
            yield return textBubble.Display("Scarecrow", "I am a spooky scary scarecrow once again!");

            yield return playerLook.LookAt(crowA.transform.position);
            yield return textBubble.Display("Crow #1", ".....");
            yield return textBubble.Display("Crow #1", "UMMM");

            crowA.GetComponent<Animation>().Play("CrowScared");
            yield return textBubble.Display("Crow #1", "WHAT");
            yield return textBubble.Display("Crow #1", "THE HECK");
            yield return textBubble.Display("Crow #1", "IS THAT");
            yield return textBubble.Display("Crow #1", "A-are you s-seeing what I'm seeing, bro?!?");

            StartCoroutine(playerLook.LookAt(crowB.transform.position));
            yield return textBubble.Display("Crow #2", "Hmmmm...");

            yield return playerLook.LookAt(transform.position + Vector3.up * 3f);
            yield return textBubble.Display("Scarecrow", "Hey");

            yield return playerLook.LookAt(crowA.transform.position);
            yield return textBubble.Display("Crow #1", "IT TALKS!!!!!!");
            yield return textBubble.Display("Crow #1", "AAAAHHHHHHH");
            yield return textBubble.Display("Crow #1", "THIs is SO SO SPOOky brO I CAN'T");

            StartCoroutine(playerLook.LookAt(crowB.transform.position));
            yield return textBubble.Display("Crow #2", "Yo you gotta calm down bro, it's literally just a pumpkin on a stick bro whatchu gettin' so upset about huh?");

            StartCoroutine(playerLook.LookAt(crowA.transform.position));
            yield return textBubble.Display("Crow #1", "Nah I c-c-can't!! IT'S FREAKIN' ME OUT HOMIE!!!!");

            StartCoroutine(playerLook.LookAt(crowB.transform.position));
            yield return textBubble.Display("Crow #2", "Hey just calm down for a second");

            StartCoroutine(playerLook.LookAt(crowA.transform.position));
            yield return textBubble.Display("Crow #1", "NO");
            yield return textBubble.Display("Crow #1", "FRICK THIS, I'M OUT");
            yield return textBubble.Display("Crow #1", "HAVE FUN WITH YOUR SPOOKY PUMPKIN BRO");

            crowKeyProp.SetActive(false);
            key.SetActive(true);

            while (crowA.transform.position.y < 10f) {
                crowA.transform.Translate(Vector3.up * Time.deltaTime * 5f);
                yield return null;
            }

            StartCoroutine(playerLook.LookAt(crowB.transform.position));
            yield return textBubble.Display("Crow #2", "Damn");
            yield return textBubble.Display("Crow #2", "What a coward");
            yield return textBubble.Display("Crow #2", "... a CROWard, am I right? Hahahahahahahhahahhaaahhah");

            yield return playerLook.LookAt(key.transform.position);
        }
        else {
            yield return textBubble.Display("", "Some kind of wooden post is firmly planted in the ground.");
        }

        EnablePlayer();
    }
}
