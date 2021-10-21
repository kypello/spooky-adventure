using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneZone : Interactable
{
    public GameObject bone;

    public ParticleSystem particles;

    public override IEnumerator Interact() {
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position));

        if (Inventory.Contains("Shovel")) {
            particles.Play();
            GetComponent<Renderer>().enabled = true;
            bone.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            yield return textBubble.Display("", "There seems to be a bone buried under the ground!");
            yield return textBubble.Display("", "The odds of us stumbling upon this by sheer chance must be incredible!");

            bone.SetActive(false);
            GetComponent<Collider>().enabled = false;

            Inventory.AddItem("Bone");
        }
        else if (Inventory.Contains("Jackhammer")) {
            yield return textBubble.Display("", "We COULD dig up the ground here with a jackhammer...");
            yield return textBubble.Display("", "...but it might damage whatever priceless treasure is buried here!");
            yield return textBubble.Display("", "And besides, we don't have a jackhammer license!");
            yield return textBubble.Display("", "We'll need to try something else...");
        }
        else {
            yield return textBubble.Display("", "This patch of ground seems kinda sus. If only we had the right tool for digging...");
        }

        EnablePlayer();
    }
}
