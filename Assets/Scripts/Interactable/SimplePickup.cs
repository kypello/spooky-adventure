using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePickup : Interactable
{
    public string itemName;

    public override IEnumerator Interact() {
        Inventory.AddItem(itemName);
        gameObject.SetActive(false);
        yield break;
    }
}
