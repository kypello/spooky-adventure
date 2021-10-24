using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravedigger : Patroller
{
    public GameObject shovelProp;
    public GameObject jackhammerProp;
    public ParticleSystem jackhammerDust;

    bool jackhammer = false;

    void Update() {
        if (hopping && !anim.isPlaying) {
            if (jackhammer) {
                anim.Play("GravediggerJackhammer");
                jackhammerDust.Play();
            }
            else {
                anim.Play("GravediggerHop");
            }
        }
    }

    public override IEnumerator StopAndChat() {
        if (jackhammer) {
            hopping = true;
        }

        if (log.Contains("giveJackhammer")) {
            yield return textBubble.Display("Gravedigger", "Can't stop to chat right now!!");

            if (Random.Range(0, 2) < 1) {
                yield return textBubble.Display("Gravedigger", "I'm on a roll!!");
            }
            else {
                yield return textBubble.Display("Gravedigger", "This is incredible!");
            }

            if (Random.Range(0, 2) < 1) {
                yield return textBubble.Display("Gravedigger", "Look at me go!");
            }
            else {
                yield return textBubble.Display("Gravedigger", "I haven't felt this young since I was 80!");
            }


        }
        else if (inventory.Contains("Jackhammer")) {
            yield return textBubble.Display("Gravedigger", "WOAH");
            yield return textBubble.Display("Gravedigger", "What is this???");
            yield return textBubble.Display("Gravedigger", "It's like a shovel on steroids or something??");
            yield return textBubble.Display("Gravedigger", "Here, take my old shovel. I wanna give 'er a whirl!");

            shovelProp.SetActive(false);
            jackhammerProp.SetActive(true);

            inventory.RemoveItem("Jackhammer");
            inventory.AddItem("Shovel");
            log.Add("giveJackhammer");
            jackhammer = true;
            defaultSpeed = 2f;
        }
        else if (log.Contains("talkedToGravedigger")) {
            yield return textBubble.Display("Gravedigger", "Hey, did you find me something better to dig with?");
            yield return textBubble.Display("Gravedigger", "If you do, I will let you borrow my old shovel.");
            yield return textBubble.Display("Gravedigger", "...");
            yield return textBubble.Display("Gravedigger", "It's super cold. Make sure you are wrapped up nice and warm...");
        }
        else {
            yield return textBubble.Display("Gravedigger", "It's a damn cold night, isn't it?");
            yield return textBubble.Display("Gravedigger", "I've been doing this job for years...");
            yield return textBubble.Display("Gravedigger", "dug more graves than I can count...");
            yield return textBubble.Display("Gravedigger", "...");
            yield return textBubble.Display("Gravedigger", "Y'know, my shovel's looking a little worn after all these years.");
            yield return textBubble.Display("Gravedigger", "I could do with an upgrade.");
            yield return textBubble.Display("Gravedigger", "If you find anything that could replace my shovel, let me know!");

            log.Add("talkedToGravedigger");
        }
    }
}

/*
yield return textBubble.Display("Gravedigger", "Every second that passes, a person will die...");
yield return textBubble.Display("Gravedigger", "Each one a story, who's pen hath run dry.");
yield return textBubble.Display("Gravedigger", "While their stories are different, their endings are not -");
yield return textBubble.Display("Gravedigger", "Buried six feet under, and left there to rot.");
yield return textBubble.Display("Gravedigger", "Thousands of stories my shovel has seen,");
yield return textBubble.Display("Gravedigger", "Though my shovel cares not to distinguish between,");
yield return textBubble.Display("Gravedigger", "Accountant or lawyer, policeman or maid,");
yield return textBubble.Display("Gravedigger", "All are the same in the eyes of my spade.");
yield return textBubble.Display("Gravedigger", "Now I've been at it for years, my joints move with a creak,");
yield return textBubble.Display("Gravedigger", "My skin's turning yellow, and my bones are quite weak.");
yield return textBubble.Display("Gravedigger", "But when my own time is up and I lay stiff as can be,");
yield return textBubble.Display("Gravedigger", "Who's shovel will be burying me?");
*/