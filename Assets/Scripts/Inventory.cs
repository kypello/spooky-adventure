using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour {
    List<string> items = new List<string>();

    TMP_Text textBox;

    void Awake() {
        textBox = GetComponent<TMP_Text>();
        UpdateInv();
    }

    public void AddItem(string item) {
        items.Add(item);
        UpdateInv();
    }

    public void RemoveItem(string item) {
        items.Remove(item);
        UpdateInv();
    }

    public bool Contains(string item) {
        return items.Contains(item);
    }

    void UpdateInv() {
        string text = "<b> Inventory</b>";

        foreach (string item in items) {
            text += "\n-" + item;
        }

        textBox.text = text;
    }
}
