using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Interactable
{
    public override IEnumerator Interact() {
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position));

        if (inventory.Contains("Fly")) {
            yield return textBubble.Display("Frog", "Hey, did you bring me a fly??");
            inventory.RemoveItem("Fly");
            yield return textBubble.Display("Frog", "*eats fly hungrily*");
            yield return textBubble.Display("Frog", "...delicious! Thank you!");

            inventory.AddItem("Frog");
            gameObject.SetActive(false);
        }
        else if (log.Contains("talkToFrog")) {
            yield return textBubble.Display("Frog", "...please find me a fly...");
            yield return textBubble.Display("Frog", "...so hungry...");
        }
        else {
            yield return textBubble.Display("Frog", "...please...");
            yield return textBubble.Display("Frog", "...find me something to eat.");
            yield return textBubble.Display("Frog", "Ever since those mean spiders came in and put these yucky webs up everywhere...");
            yield return textBubble.Display("Frog", "there haven't been enough flies for us frogs to eat...");
            yield return textBubble.Display("Frog", "If you can find me a tasty fly to eat, I will let you pick me up!");
            log.Add("talkToFrog");
        }

        EnablePlayer();
    }
}
