using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : Button
{
    public Pause pause;

    public override void Click() {
        pause.SetPausedState(false);
        pointerOver = false;
        text.color = defaultColor;
    }
}
