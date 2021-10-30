using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frankenstein : Interactable
{
    public CollectCandy collectCandy;
    bool talkedTo = false;
    bool seen300 = false;

    public Animation door;
    public Collider mansionCollisionDefault;
    public Collider mansionCollisionDoorOpen;
    public Collider doorTrigger;

    public bool doorPermanentlyOpen = false;
    public AudioSource doorOpen;

    public override IEnumerator Interact() {
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 3f));

        if (doorPermanentlyOpen) {
            if (collectCandy.candy >= 300 && !seen300) {
                seen300 = true;
                yield return textBubble.Display("Franklin", "...nice! You finally found all of 'em!");
                yield return textBubble.Display("Franklin", "...That means you've got a one-way ticket the VIP lounge!");
            }
            yield return textBubble.Display("Franklin", "What are you waiting for? Get yer ass into that party bro!");
        }
        else {
            yield return textBubble.Display("Franklin", "Halt!");

            if (!talkedTo) {
                yield return textBubble.Display("Franklin", "This party is stricty MONSTERS ONLY! No humans allowed!");
                yield return textBubble.Display("Franklin", "...unless you were to collect 250 candies, in which case we can make an exception.");
                yield return textBubble.Display("Franklin", "And if you can collect all 300, you will be allowed access to the VIP lounge.");
            }

            if (collectCandy.candy >= 300) {
                yield return textBubble.Display("Franklin", "...wow! You've collected all 300 candies!");
                OpenDoor();
                yield return textBubble.Display("Franklin", "This means you can waddle your merry way over to the VIP lounge!");
                yield return textBubble.Display("Franklin", "Enjoy!");
                seen300 = true;
            }
            else if (collectCandy.candy >= 250) {
                yield return textBubble.Display("Franklin", "...seems you've collected " + collectCandy.candy + " candies! Nice!");
                OpenDoor();
                yield return textBubble.Display("Franklin", "With that, you may enter the party! Have fun!");
                yield return textBubble.Display("Franklin", "...but there's still " + (300 - collectCandy.candy) + (collectCandy.candy == 299 ? " candy" : " candies") + " left out there! Can you find " + (collectCandy.candy == 299 ? "it?" : "them?"));
            }
            else {
                yield return textBubble.Display("Franklin", "...you've only got " + collectCandy.candy + (collectCandy.candy == 1 ? " candy" : " candies") + " with you.");

                if (talkedTo) {
                    yield return textBubble.Display("Franklin", "Come back when you've found at least 250 if you want in.");
                    yield return textBubble.Display("Franklin", "...and if you want access to the VIP lounge, bring me all 300!");
                }
                else {
                    yield return textBubble.Display("Franklin", "Come back when you've found some more.");
                    talkedTo = true;
                }
            }
        }

        EnablePlayer();
    }

    void OpenDoor() {
        doorPermanentlyOpen = true;
        door.Play("DoorOpen");
        doorOpen.Play();

        mansionCollisionDefault.enabled = false;
        mansionCollisionDoorOpen.enabled = true;
        //doorTrigger.enabled = false;

        StartCoroutine(playerLook.LookAt(door.transform.position + Vector3.up * 2f));
    }
}
