using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : Interactable
{
    Animation anim;
    public GameObject jackhammer;

    void Start() {
        anim = GetComponent<Animation>();
    }

    public override IEnumerator Interact() {
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 2f));

        if (inventory.Contains("Coin")) {
            inventory.RemoveItem("Coin");
            yield return textBubble.Display("Wishing Well", "Thank you!");
            yield return textBubble.Display("Wishing Well", "Now think hard about what you want to wish for...");
            yield return textBubble.Display("Wishing Well", "...");
            yield return textBubble.Display("Wishing Well", "...yes! I can see it! I can see what it is you desire above all else!");
            anim.Play();
            yield return textBubble.Display("Wishing Well", "...");
            yield return textBubble.Display("Wishing Well", "...one deluxe-model high-powered jackhammer!");
            while (anim.isPlaying) {
                yield return null;
            }

            jackhammer.SetActive(false);
            inventory.AddItem("Jackhammer");

            yield return textBubble.Display("Wishing Well", "Thanks for wishing! Come again!");
        }
        else {
            yield return textBubble.Display("Wishing Well", "I can look into your mind and give you what your heart desires most...");
            yield return textBubble.Display("Wishing Well", "...if you insert one gold coin.");
        }

        EnablePlayer();
    }
}
