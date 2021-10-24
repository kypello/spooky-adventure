using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatroller : Patroller
{
    public string name;
    public string[] lines;

    public override IEnumerator StopAndChat() {
        foreach (string line in lines) {
            yield return textBubble.Display(name, line);
        }
    }
}
