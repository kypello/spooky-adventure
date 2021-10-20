using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public static TextBubble textBubble;
    public static Player player;
    public static PlayerLook playerLook;
    public static PlayerInteract playerInteract;

    public void Awake() {
        textBubble = FindObjectOfType<TextBubble>();
        player = FindObjectOfType<Player>();
        playerLook = FindObjectOfType<PlayerLook>();
        playerInteract = FindObjectOfType<PlayerInteract>();
    }

    public void DisablePlayer() {
        player.control = false;
        playerLook.control = false;
        playerInteract.control = false;
    }

    public void EnablePlayer() {
        player.control = true;
        playerLook.control = true;
        playerInteract.control = true;
    }

    public abstract IEnumerator Interact();
}