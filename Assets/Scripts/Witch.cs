using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Interactable
{
    public Animation itemDrop;

    public GameObject[] itemModels;
    public Color[] colors;
    public Color[] altColors;

    public ParticleSystem bubbles;
    public ParticleSystem smoke;
    public ParticleSystem burst;

    public Material potionMat;

    void Start() {
        SetPotionColors(colors[0], altColors[0]);
    }

    public override IEnumerator Interact() {
        DisablePlayer();
        yield return textBubble.Display("Witch", "I am a witch hehehehehe");
        yield return DropItemInPotion(1);
        EnablePlayer();
    }

    IEnumerator DropItemInPotion(int item) {
        foreach (GameObject itemModel in itemModels) {
            itemModel.SetActive(false);
        }

        itemModels[item].SetActive(true);

        itemDrop.Play();
        yield return new WaitForSeconds(0.75f);

        SetPotionColors(colors[item], altColors[item]);

        burst.Play();
    }

    void SetPotionColors(Color a, Color b) {
        ParticleSystem.MinMaxGradient minMaxGradient = new ParticleSystem.MinMaxGradient(a, b);

        ParticleSystem.MainModule main = bubbles.main;
        main.startColor = minMaxGradient;

        minMaxGradient = new ParticleSystem.MinMaxGradient(new Color(a.r, a.g, a.b, 0.4f), new Color(b.r, b.g, b.b, 0.4f));

        main = smoke.main;
        main.startColor = minMaxGradient;

        main = burst.main;
        main.startColor = minMaxGradient;

        potionMat.SetColor("_Background", a);
        potionMat.SetColor("_Swirl", b);
    }
}
