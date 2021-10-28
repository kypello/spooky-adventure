using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LineBetweenPoints : MonoBehaviour
{
    public Transform a;
    public Transform b;

    LineRenderer rend;

    void OnEnable() {
        rend = GetComponent<LineRenderer>();
    }

    void Update() {
        rend.SetPositions(new Vector3[] { a.position, b.position });
    }
}
