using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warewolf : Interactable
{
    Animation anim;

    bool talkingAnimPlaying = false;

    void Start() {
        anim = GetComponent<Animation>();
    }

    public override IEnumerator Interact() {
        DisablePlayer();

        StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 3f));
        
        if (Inventory.Contains("Shovel")) {
            yield return textBubble.Display("Where Wolf", "Is that a shovel you're carrying?");
            yield return textBubble.Display("Where Wolf", "Looks like the perfect tool for digging up the bone that I hid!");
            yield return textBubble.Display("Where Wolf", "But you could never possibly do that of course...");
            yield return textBubble.Display("Where Wolf", "Because you don't know that it's buried in between the wishing well and the rock.");
            yield return textBubble.Display("Where Wolf", "And so my bone remains expertly hidden...");
            yield return textBubble.Display("Where Wolf", "Because the top-secret location, in between the wishing well and the rock, is top-secret.");
        }
        else if (log.Contains("talkedToWerewolf")) {
            yield return textBubble.Display("Where Wolf", "I'm so happy that no one knows where my bone is buried...");
            yield return textBubble.Display("Where Wolf", "Yep, absolutely know one except me knows about it's secret location between the wishing well and the rock");
            yield return textBubble.Display("Where Wolf", "If someone were to find out that it's between the wishing well and the rock, then I would be in trouble...");
            yield return textBubble.Display("Where Wolf", "But that will never happen! Because I am the best at keeping secrets!!");
        }
        else {
            yield return textBubble.Display("Where Wolf", "My bone is buried somewhere");
            yield return textBubble.Display("Where Wolf", "Will anybody ever find it?");
            yield return textBubble.Display("Where Wolf", "No they won't, because they don't know that it's buried directly between the wishing well and the rock!");
            yield return textBubble.Display("Where Wolf", "I bet you wish you knew where my bone is, so you could steal it");
            yield return textBubble.Display("Where Wolf", "Too bad you don't know it's location, which is between the wishing well and the rock...");
            yield return textBubble.Display("Where Wolf", "If somebody knew that, however...");
            yield return textBubble.Display("Where Wolf", "They could dig it up!");
            yield return textBubble.Display("Where Wolf", "And then I'd have to find another bone! Wouldn't that be a shame...");
            yield return textBubble.Display("Where Wolf", "I'd better keep it's location a secret!!");
            log.Add("talkedToWerewolf");
        }

        EnablePlayer();
    }
}
