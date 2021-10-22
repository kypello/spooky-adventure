using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardLock : Interactable
{
    public Animation anim;

    public override IEnumerator Interact() {
        if (inventory.Contains("GraveyardKey")) {
            anim.Play();
            GetComponent<Collider>().enabled = false;
            inventory.RemoveItem("GraveyardKey");
        }
        else {
            DisablePlayer();
            yield return textBubble.Display("", "The gate is locked with an ornate golden lock.");
            EnablePlayer();
        }
    }
}
