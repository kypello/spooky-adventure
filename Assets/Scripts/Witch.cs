using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Interactable
{
    public override IEnumerator Interact() {
        DisablePlayer();
        yield return textBubble.Display("Witch", "I am a witch hehehehehe");
        EnablePlayer();
    }
}
