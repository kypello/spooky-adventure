using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public GameObject[] candyPrefabs;

    void Awake() {
        GameObject candyPrefab = candyPrefabs[Random.Range(0, candyPrefabs.Length)];
        Instantiate(candyPrefab, transform);
    }
}
