using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsButton : Button
{
    public Camera cam;
    public GameObject postProcessing;

    bool fastGraphics;

    public override void Click() {
        fastGraphics = !fastGraphics;

        if (fastGraphics) {
            text.text = "<b>Graphics:</b> Fast";

            cam.farClipPlane = 50f;
            postProcessing.SetActive(false);
            QualitySettings.SetQualityLevel(0, true);
        }
        else {
            text.text = "<b>Graphics:</b> Fancy";

            cam.farClipPlane = 200f;
            postProcessing.SetActive(true);
            QualitySettings.SetQualityLevel(1, true);
        }
    }
}
