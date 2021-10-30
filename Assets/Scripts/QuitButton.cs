using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : Button
{
    public override void Click() {
        Application.Quit();
    }
}
