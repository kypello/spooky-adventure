using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowB : Interactable {
    public override IEnumerator Interact() {
        DisablePlayer();

        StartCoroutine(playerLook.LookAt(transform.position));
        yield return textBubble.Display("Crow #2", "I'm not holding anything right now, but I might in the future.");

        EnablePlayer();
    }
}
