using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowHead : Interactable
{
    public Animation anim;

    void Update() {
        Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);

        if (Vector3.Distance(transform.position, playerPos) < 20f) {
            Vector3 dirToPlayer = (playerPos - transform.position).normalized;

            if (Vector3.Dot(transform.forward, dirToPlayer) < 0.99f) {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, dirToPlayer, Mathf.PI * 0.5f * Time.deltaTime, 0f));

                if (!anim.isPlaying) {
                    anim.Play();
                }
            }
        }
    }

    public override IEnumerator Interact() {
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position));

        anim.Play();
        yield return textBubble.Display("Pumpkin", "Finally! Someone noticed me down here!!");
        anim.Play();
        yield return textBubble.Display("Pumpkin", "Hey, do me a real solid, would ya? There's a wooden post-looking thing in the pumpkin patch, see...");
        anim.Play();
        yield return textBubble.Display("Pumpkin", "...would you mind carrying me over there and placing me on top? Please and thank you and all that!");

        Inventory.AddItem("PumpkinHead");
        EnablePlayer();
        gameObject.SetActive(false);
    }
}
