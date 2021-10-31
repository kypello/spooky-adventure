using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScoreDisplay : MonoBehaviour
{
    public GameObject star;
    TMP_Text text;

    void Awake() {
        text = GetComponent<TMP_Text>();
        text.text = "Candies Collected: " + CollectCandy.candy + "/300";
        if (CollectCandy.candy >= 300) {
            star.SetActive(true);
        }
    }
}
