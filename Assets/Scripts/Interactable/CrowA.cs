using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowA : Interactable
{
    public override IEnumerator Interact() {
        DisablePlayer();

        StartCoroutine(playerLook.LookAt(transform.position));
        yield return textBubble.Display("Crow #1", "I found this key, isn't it pretty??");
        yield return textBubble.Display("Crow #1", "I'm not letting anyone touch it. I found it and I'm holding onto it tight!");
        yield return textBubble.Display("Crow #1", "Unless, like, something REALLY SCARY happens, I'm not letting go!");

        EnablePlayer();
    }
}
