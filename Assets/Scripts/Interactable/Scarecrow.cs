using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : Interactable
{
    public GameObject pumpkin;
    public GameObject hat;

    public GameObject crowA;
    public GameObject crowB;
    public GameObject crowKeyProp;
    public GameObject key;
    public GameObject crowCoinsProp;
    public GameObject coins;

    public override IEnumerator Interact() {
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 3f));

        if (log.Contains("hatPlaced")) {
            yield return textBubble.Display("Scarecrow", "Thanks again for the hat!");
            yield return textBubble.Display("Scarecrow", "Does it make me look scary?");
            yield return textBubble.Display("Scarecrow", "On a scale of 1 to SPOOK, how scary do I look right now?");
        }
        if (inventory.Contains("ScarecrowHat")) {
            inventory.RemoveItem("ScarecrowHat");
            hat.SetActive(true);
            log.Add("hatPlaced");

            yield return textBubble.Display("Scarecrow", "I love it!");
            yield return textBubble.Display("Scarecrow", "Thanks for bringing me this hat!");

            yield return playerLook.LookAt(crowB.transform.position);

            yield return textBubble.Display("Crow #2", "...woah...");
            yield return textBubble.Display("Crow #2", ".....");
            yield return textBubble.Display("Crow #2", "See, when it was a pumpkin on a stick, I could deal with that...");
            yield return textBubble.Display("Crow #2", "...but with a hat thrown into the mix...");

            crowB.GetComponent<Animation>().Play("CrowScared");

            yield return textBubble.Display("Crow #2", "...A LINE HAS DEFINITELY BEEN CROSSED!!");
            yield return textBubble.Display("Crow #2", "And I am NOT comfortable with this!!");
            yield return textBubble.Display("Crow #2", "OH MY GOD THAT IS SO SCARYYYYYYYYYYYYY :O");
            yield return textBubble.Display("Crow #2", "I'M DONE");

            crowCoinsProp.SetActive(false);
            coins.SetActive(true);

            while (crowB.transform.position.y < 10f) {
                crowB.transform.Translate(Vector3.up * Time.deltaTime * 5f);
                yield return null;
            }
            crowB.SetActive(false);

            yield return playerLook.LookAt(coins.transform.position);
        }
        else if (log.Contains("pumpkinPlaced")) {
            yield return textBubble.Display("Scarecrow", "My look isn't complete until I have a hat!");
            yield return textBubble.Display("Scarecrow", "If you find one, please bring it over to here!");
        }
        else if (inventory.Contains("PumpkinHead")) {
            inventory.RemoveItem("PumpkinHead");
            pumpkin.SetActive(true);
            log.Add("pumpkinPlaced");

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
            crowA.SetActive(false);

            StartCoroutine(playerLook.LookAt(crowB.transform.position));
            yield return textBubble.Display("Crow #2", "Damn");
            yield return textBubble.Display("Crow #2", "What a coward");
            yield return textBubble.Display("Crow #2", "... a CROWard, am I right? Hahahahahahahhahahhaaahhah");

            yield return playerLook.LookAt(transform.position + Vector3.up * 3f);

            yield return textBubble.Display("Scarecrow", "I don't want to be too demanding now, but can I please ask for one more thing?");
            yield return textBubble.Display("Scarecrow", "Could you please find me a hat to wear?");
        }
        else {
            yield return textBubble.Display("", "Some kind of wooden post is firmly planted in the ground.");
        }

        EnablePlayer();
    }
}
