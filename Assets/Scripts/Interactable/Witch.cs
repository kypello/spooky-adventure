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

    public Light light;
    public Animation lightAnim;

    public Material potionMat;

    void Start() {
        SetPotionColors(colors[0], altColors[0]);
    }

    public override IEnumerator Interact() {
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 2.5f));

        yield return textBubble.Display("Witch", "I am a witch hehehehehe");

        StartCoroutine(playerLook.LookAt(bubbles.transform.position));

        yield return DropItemInPotion(1);
        EnablePlayer();
    }

    IEnumerator DropItemInPotion(int item) {
        foreach (GameObject itemModel in itemModels) {
            itemModel.SetActive(false);
        }

        itemModels[item].SetActive(true);

        itemDrop.Play();
        yield return new WaitForSeconds(0.7f);

        SetPotionColors(colors[item], altColors[item]);

        burst.Play();
        lightAnim.Play();

        float ripple = 0f;
        while (ripple < 1.5f) {
            yield return null;
            ripple += Time.deltaTime;
            potionMat.SetFloat("_RippleDist", ripple);
        }
        potionMat.SetFloat("_RippleDist", -2f);
    }

    void SetPotionColors(Color a, Color b) {
        ParticleSystem.MinMaxGradient minMaxGradient = new ParticleSystem.MinMaxGradient(a, b);

        ParticleSystem.MainModule main = bubbles.main;
        main.startColor = minMaxGradient;

        main = burst.main;
        main.startColor = minMaxGradient;

        minMaxGradient = new ParticleSystem.MinMaxGradient(new Color(a.r, a.g, a.b, 0.4f), new Color(b.r, b.g, b.b, 0.4f));

        main = smoke.main;
        main.startColor = minMaxGradient;

        potionMat.SetColor("_Background", a);
        potionMat.SetColor("_Swirl", b);

        light.color = a;
    }
}