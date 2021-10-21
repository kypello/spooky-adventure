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
        
        //mushroom
        if (!log.Contains("mushroomInPotion") && (!Inventory.Contains("Mushroom") || !log.Contains("heardMushroomRiddle"))) {
            StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 2.5f));
            yield return textBubble.Display("Witch", "A white-speck'd cap, a slender stalk...");
            yield return textBubble.Display("Witch", "...bring me a red mushroom, then we'll talk.");
            log.Add("heardMushroomRiddle");
        }

        if (Inventory.Contains("Mushroom")) {
            StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 2.5f));
            yield return textBubble.Display("Witch", "You've got it! You've got it! It's just what I need!");
            yield return textBubble.Display("Witch", "This fungus will add a strong flavour indeed!");

            StartCoroutine(playerLook.LookAt(bubbles.transform.position));
            yield return DropItemInPotion(1);
            log.Add("mushroomInPotion");
        }

        //bone
        if (!log.Contains("boneInPotion") && (!Inventory.Contains("Bone") || !log.Contains("heardBoneRiddle"))) {
            StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 2.5f));
            yield return textBubble.Display("Witch", "A human bone, fresh from the grave...");
            yield return textBubble.Display("Witch", "...will surely make this potion behave.");
            log.Add("heardBoneRiddle");
        }

        if (Inventory.Contains("Bone")) {
            StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 2.5f));
            yield return textBubble.Display("Witch", "A bone! A bone! Toss it in, quick!");
            yield return textBubble.Display("Witch", "The marrow must boil, or it will make you quite sick!");

            StartCoroutine(playerLook.LookAt(bubbles.transform.position));
            yield return DropItemInPotion(2);
            log.Add("boneInPotion");
        }

        //frog
        if (!log.Contains("frogInPotion") && (!Inventory.Contains("Frog") || !log.Contains("heardFrogRiddle"))) {
            StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 2.5f));
            yield return textBubble.Display("Witch", "We're almost there, so I'll give you a clue:");
            yield return textBubble.Display("Witch", "I'll need a creature that croaks to finish this brew!");
            log.Add("heardFrogRiddle");
        }

        if (Inventory.Contains("Frog")) {
            StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 2.5f));
            yield return textBubble.Display("Witch", "A frog! There it is! In the cauldron it goes...");
            yield return textBubble.Display("Witch", "...along with some herbs and some spices and buffalo toes...");

            StartCoroutine(playerLook.LookAt(bubbles.transform.position));
            yield return DropItemInPotion(3);
            log.Add("frogInPotion");

            StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 2.5f));
            yield return textBubble.Display("Witch", "...and that's it! We're done! Our concoction's complete!");
            yield return textBubble.Display("Witch", "With a bit of ice cream, it's a wonderful treat!");

            yield return textBubble.Display("Witch", "...");
            yield return textBubble.Display("Witch", "Ok, no more rhyming.");
            yield return textBubble.Display("Witch", "God, I HATE having to speak in rhymes.");
            yield return textBubble.Display("Witch", "Worst part of potion making by far.");
            yield return textBubble.Display("Witch", "Oh, by the way, this is just some cough medicine. It will give you a raspy cough, if your throat is too clear.");
            yield return textBubble.Display("Witch", "Very trendy among monsters these days.");
            yield return textBubble.Display("Witch", "*drinks cough medicine*");
            yield return textBubble.Display("Witch", "...looks like I have some left. You can take it. I'm on break now. Cya m8");
        }


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
