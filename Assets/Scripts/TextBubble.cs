using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBubble : MonoBehaviour
{
    public GameObject bubble;
    public TMP_Text tmp;
    public TMP_Text tmpName;

    public IEnumerator Display(string name, string text) {
        bubble.SetActive(true);

        tmpName.text = name;

        tmp.text = text;
        Canvas.ForceUpdateCanvases();
        int lineCountTotal = tmp.textInfo.lineCount;
        tmp.text = "";

        int characters = text.Length;

        for (int i = 0; i <= characters; i++) {
            tmp.text = text.Substring(0, i);
            Canvas.ForceUpdateCanvases();
            int lineCount = tmp.textInfo.lineCount;
            for (int j = lineCount; j < lineCountTotal; j++) {
                tmp.text += "\n ";
            }
            yield return null;

            if (Input.GetMouseButtonDown(0)) {
                i = characters - 1;
            }
        }

        do {
            yield return null;
        } while (!Input.GetMouseButtonDown(0));

        bubble.SetActive(false);
    }
}
