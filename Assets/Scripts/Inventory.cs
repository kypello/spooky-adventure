using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory
{
    static List<string> items = new List<string>();

    public static void AddItem(string item) {
        items.Add(item);
    }

    public static void RemoveItem(string item) {
        items.Remove(item);
    }

    public static bool Contains(string item) {
        return items.Contains(item);
    }
}
