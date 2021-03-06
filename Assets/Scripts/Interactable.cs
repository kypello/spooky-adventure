using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public static TextBubble textBubble;
    public static Player player;
    public static PlayerLook playerLook;
    public static PlayerInteract playerInteract;

    static Pause pause;

    protected static List<string> log = new List<string>();

    public void Awake() {
        textBubble = FindObjectOfType<TextBubble>();
        player = FindObjectOfType<Player>();
        playerLook = FindObjectOfType<PlayerLook>();
        playerInteract = FindObjectOfType<PlayerInteract>();

        pause = FindObjectOfType<Pause>();
    }

    public void DisablePlayer() {
        pause.SetPausedState(false);

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
