using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : Interactable
{
    Animation anim;

    void Start() {
        anim = GetComponent<Animation>();
    }

    public override IEnumerator Interact() {
        yield break;
    }
}
