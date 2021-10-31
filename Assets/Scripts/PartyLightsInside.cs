using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyLightsInside : MonoBehaviour
{
    public Color a;
    public Color b;
    public float timeDelay;
    public Light light;
    public Camera cam;

    void Start() {
        StartCoroutine(Lights());
    }

    IEnumerator Lights() {
        while (true) {
            SetColor(a);
            yield return new WaitForSeconds(timeDelay);
            SetColor(b);
            yield return new WaitForSeconds(timeDelay);
        }
    }

    void SetColor(Color c) {
        light.color = c;
        cam.backgroundColor = c * 0.4f;
        RenderSettings.ambientLight = c;
        RenderSettings.fogColor = c * 0.4f;
    }
}
