using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : Button
{
    public bool playing;

    public GameObject title;
    public GameObject crosshair;
    public Pause pause;

    public override void Click() {
        playing = true;
        title.SetActive(false);
        crosshair.SetActive(true);
        pause.SetPausedState(false);
        gameObject.SetActive(false);
    }
}
