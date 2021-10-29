using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frankenstein : Interactable
{
    public CollectCandy collectCandy;
    bool talkedTo = false;
    bool allowedIn = false;

    public override IEnumerator Interact() {
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 4f));

        if (allowedIn) {
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
                yield return textBubble.Display("Franklin", "This means you can waddle your merry way over to the VIP lounge!");
                yield return textBubble.Display("Franklin", "Enjoy!");
                allowedIn = true;
            }
            else if (collectCandy.candy >= 250) {
                yield return textBubble.Display("Franklin", "...seems you've collected " + collectCandy.candy + " candies! Nice!");
                yield return textBubble.Display("Franklin", "With that, you may enter the party!");
                yield return textBubble.Display("Franklin", "...but there's still " + (300 - collectCandy.candy) + " candies left out there! Can you find them?");
                allowedIn = true;
            }
            else {
                yield return textBubble.Display("Franklin", "...you've only got " + collectCandy.candy + (collectCandy.candy == 1 ? " candy" : " candies") + " with you.");

                if (talkedTo) {
                    yield return textBubble.Display("Franklin", "Come back when you've found at least 250 if you want in.");
                    yield return textBubble.Display("Franklin", "...and if you want access to the VIP lounge, bring me all 300!");
                }
                else {
                    talkedTo = true;
                }
            }
        }

        EnablePlayer();
    }
}
