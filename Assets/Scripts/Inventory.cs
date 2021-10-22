using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    List<string> items = new List<string>();

    [System.Serializable]
    public struct ItemPrefab {
        public GameObject prefab;
        public string itemName;
    }
    public List<ItemPrefab> itemPrefabs = new List<ItemPrefab>();

    public List<Transform> itemSpawns = new List<Transform>();

    List<GameObject> itemObjects = new List<GameObject>();

    public void AddItem(string item) {
        items.Add(item);

        GameObject prefab = FindItemPrefab(item);
        GameObject newItem = Instantiate(prefab, itemSpawns[items.Count - 1].position, prefab.transform.rotation);
        itemObjects.Add(newItem);
    }

    public void RemoveItem(string item) {
        int index = items.IndexOf(item);

        items.Remove(item);
        items.Remove(item);

        for (int i = index; i < itemObjects.Count; i++) {
            itemObjects[i].transform.position = itemSpawns[i].position;
        }
    }

    public void ReplaceItem(string old, string neww) {
        int index = items.IndexOf(old);
        items[index] = neww;

        Destroy(itemObjects[index]);

        GameObject prefab = FindItemPrefab(neww);
        GameObject newItem = Instantiate(prefab, itemSpawns[index].position, prefab.transform.rotation);

        itemObjects[index] = newItem;
    }

    public bool Contains(string item) {
        return items.Contains(item);
    }

    GameObject FindItemPrefab(string itemName) {
        foreach (ItemPrefab itemPrefab in itemPrefabs) {
            if (itemPrefab.itemName == itemName) {
                return itemPrefab.prefab;
            }
        }
        return null;
    }
}
