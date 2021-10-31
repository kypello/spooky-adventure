using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyCursorLockSet : MonoBehaviour
{
    void Start() {
        Cursor.lockState = CursorLockMode.None;
    }
}
