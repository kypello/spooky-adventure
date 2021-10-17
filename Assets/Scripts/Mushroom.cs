using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Interactable
{
    public override IEnumerator Interact() {
        Inventory.AddItem("Mushroom");
        gameObject.SetActive(false);
        yield break;
    }
}
