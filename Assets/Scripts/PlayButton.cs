using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : Button
{
    public bool playing;

    public GameObject title;
    public GameObject crosshair;
    public GameObject credits;
    public GameObject pauseControls;
    public Pause pause;

    public override void Click() {
        playing = true;
        title.SetActive(false);
        crosshair.SetActive(true);
        credits.SetActive(false);
        pauseControls.SetActive(true);
        pause.SetPausedState(false);
        gameObject.SetActive(false);
    }
}
