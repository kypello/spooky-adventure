using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : Interactable
{
    public GameObject pumpkin;

    bool pumpkinPlaced = false;

    public override IEnumerator Interact() {
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 3f));

        if (pumpkinPlaced) {
            yield return textBubble.Display("Scarecrow", "Please find me something to wear!!");
        }
        else if (Inventory.Contains("PumpkinHead")) {
            Inventory.RemoveItem("PumpkinHead");
            pumpkin.SetActive(true);
            pumpkinPlaced = true;

            yield return textBubble.Display("Scarecrow", "Thanks dog!! Here is the key to the graveyard!");

            Inventory.AddItem("GraveyardKey");

            yield return textBubble.Display("Scarecrow", "Now I feel a bit exposed here, could you find me some kind of outfit plsssss");
        }
        else {
            yield return textBubble.Display("", "Some kind of wooden post is firmly planted in the ground.");
        }

        EnablePlayer();
    }
}
