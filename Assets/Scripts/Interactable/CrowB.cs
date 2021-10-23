using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowB : Interactable {
    public override IEnumerator Interact() {
        DisablePlayer();

        StartCoroutine(playerLook.LookAt(transform.position));
        if (log.Contains("pumpkinPlaced")) {
            yield return textBubble.Display("Crow #2", "How could someone be scared of a pumpkin on a stick???");
            yield return textBubble.Display("Crow #2", "(at least it doesn't have a hat though. That would REALLY shiver my timbers...)");
        }
        else {
            yield return textBubble.Display("Crow #2", "I found this coin.");
            yield return textBubble.Display("Crow #2", "And I'm not letting go.");
        }

        EnablePlayer();
    }
}
