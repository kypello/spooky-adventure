using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public Player player;
    public PlayerLook playerLook;
    public PlayerInteract playerInteract;

    public GameObject pauseScreen;

    bool paused;

    void Update() {
        if (!paused && player.control && Input.GetKeyDown(KeyCode.P)) {
            SetPausedState(true);
        }
        else if (paused && Input.GetKeyDown(KeyCode.P)) {
            SetPausedState(false);
        }
    }

    void SetPausedState(bool p) {
        paused = p;
        player.control = !p;
        playerLook.control = !p;
        playerInteract.control = !p;

        pauseScreen.SetActive(p);
    }
}
