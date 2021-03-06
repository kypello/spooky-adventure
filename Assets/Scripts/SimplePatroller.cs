using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatroller : Patroller
{
    public string name;

    int interactions = 0;

    [System.Serializable]
    public struct LinesArray {
        [TextArea]
        public string[] lines;
    }

    public LinesArray[] lines;

    public override IEnumerator StopAndChat() {
        foreach (string line in lines[interactions].lines) {
            yield return textBubble.Display(name, line);
        }

        if (interactions < lines.Length - 1) {
            interactions++;
        }
    }
}
