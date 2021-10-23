using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : Interactable
{
    Animation anim;
    public Transform skull;
    bool talking;

    void Start() {
        anim = GetComponent<Animation>();
    }

    void Update() {
        Vector3 playerZero = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        transform.rotation = Quaternion.LookRotation(playerZero - transform.position, Vector3.up);

        skull.rotation = Quaternion.LookRotation(player.transform.position - skull.transform.position, Vector3.up);

        if (talking && !anim.isPlaying) {
            anim.Play();
        }
    }

    public override IEnumerator Interact() {
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 3f));

        talking = true;

        if (log.Contains("givenScythe")) {
            yield return textBubble.Display("Grimm", "How's it going with the scythe?");
            yield return textBubble.Display("Grimm", "If you cut yourself I have some plasters here");
        }
        else if (log.Contains("thirdCardGiven")) {
            yield return textBubble.Display("Grimm", "Hey! I got good new and bad news...");
            yield return textBubble.Display("Grimm", "Good news is I've just gotten three calls from people who got those business cards!");
            yield return textBubble.Display("Grimm", "Bad news is, none of them were quite what I'm looking for...");
            yield return textBubble.Display("Grimm", "Oh well. A deal's a deal, and I'm a man(?) of my word. The scythe is yours.");
            inventory.AddItem("Scythe");
            log.Add("givenScythe");
            yield return textBubble.Display("Grimm", "Just one rule - no reaping souls! You need a degree from Reaper University and a Reaper License...");
            yield return textBubble.Display("Grimm", "...not to mention, hours and hours of training...");
            yield return textBubble.Display("Grimm", "...but other than that, use it for whatever you like!");
            yield return textBubble.Display("Grimm", "Like, I dunno, if you have some bread that needs cutting or something, just go to town with the ol' scythe!");
            yield return textBubble.Display("Grimm", "Have fun...");
        }
        else if (log.Contains("receivedBusinessCards")) {
            yield return textBubble.Display("Grimm", "Looks like you've still got some business cards there...");
            yield return textBubble.Display("Grimm", "Keep handing them out, and come back when they're all gone!");
        }
        else {
            yield return textBubble.Display("Grimm", "....OH! A customer!! Welcome!!!");
            yield return textBubble.Display("Grimm", "If you're looking for a quick and painless journey to the afterlife, you've found the right reaper!");
            yield return textBubble.Display("Grimm", "...huh?");
            yield return textBubble.Display("Grimm", "...you're not ready to die yet?");
            yield return textBubble.Display("Grimm", "*sign*");
            yield return textBubble.Display("Grimm", "It doesn't seem like ANYBODY is anymore...");
            yield return textBubble.Display("Grimm", "Business has been very slow as of late.");
            yield return textBubble.Display("Grimm", "So slow, that I've left the intern covering my shift at the Purgatory Checkpoint and they are holding up just fine.");
            yield return textBubble.Display("Grimm", "Hey, would you do me a favour?");

            inventory.AddItem("BusinessCard");
            inventory.AddItem("BusinessCard");
            inventory.AddItem("BusinessCard");
            log.Add("receivedBusinessCards");
            log.Add("thirdCardGiven");

            yield return textBubble.Display("Grimm", "I've got some business cards here, would you mind handing them out to anyone who might like them?");
            yield return textBubble.Display("Grimm", "Sick, elderly, anyone who's just getting bored of the living world... doesn't matter.");
            yield return textBubble.Display("Grimm", "If they want a safe and comfortable journey to the afterlife, tell them to give me a call!");
            yield return textBubble.Display("Grimm", "If you do this for me, I will let you borrow my trusty scythe for a bit! Sounds fair?");
            yield return textBubble.Display("Grimm", "Thanks a bunch!");
        }

        talking = false;

        EnablePlayer();
    }
}
