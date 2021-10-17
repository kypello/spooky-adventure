using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Interactable
{
    public Animation itemDrop;

    public GameObject[] itemModels;
    public Color[] colors;
    public Color[] altColors;

    public ParticleSystem[] particles;
    public ParticleSystem burst;

    public Material potionMat;

    public override IEnumerator Interact() {
        DisablePlayer();
        yield return textBubble.Display("Witch", "I am a witch hehehehehe");
        yield return DropItemInPotion(0);
        EnablePlayer();
    }

    IEnumerator DropItemInPotion(int item) {
        foreach (GameObject itemModel in itemModels) {
            itemModel.SetActive(false);
        }

        itemModels[item].SetActive(true);

        itemDrop.Play();
        yield return new WaitForSeconds(0.75f);

        ParticleSystem.MinMaxGradient minMaxGradient = new ParticleSystem.MinMaxGradient(colors[item], altColors[item]);

        foreach (ParticleSystem particleSystem in particles) {
            ParticleSystem.MainModule main = particleSystem.main;
            main.startColor = minMaxGradient;
        }

        burst.Play();

        potionMat.SetColor("_Background", colors[item]);
        potionMat.SetColor("_Swirl", colors[item]);
    }
}
