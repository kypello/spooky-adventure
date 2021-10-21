using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravedigger : Interactable
{
    public Transform[] patrolPoints;

    public float speed = 1f;

    float defaultSpeed = 1f;

    bool hopping = true;
    public Animation anim;

    public GameObject shovelProp;
    public GameObject jackhammerProp;
    public ParticleSystem jackhammerDust;

    bool jackhammer = false;

    void Start() {
        StartCoroutine(Patrol());
    }

    void Update() {
        if (hopping && !anim.isPlaying) {
            if (jackhammer) {
                anim.Play("GravediggerJackhammer");
                jackhammerDust.Play();
            }
            else {
                anim.Play("GravediggerHop");
            }
        }
    }

    public override IEnumerator Interact() {
        speed = 0f;
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position + Vector3.up * 2f));

        Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 dirToPlayer = (playerPos - transform.position).normalized;

        Vector3 prevDir = transform.forward;

        while (Vector3.Dot(transform.forward, dirToPlayer) < 0.999f) {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, dirToPlayer, Mathf.PI * 0.5f * Time.deltaTime, 0f));
            yield return null;
        }

        if (!jackhammer) {
            hopping = false;
        }

        if (log.Contains("giveJackhammer")) {
            yield return textBubble.Display("Gravedigger", "Can't stop to chat right now!!");

            if (Random.Range(0, 2) < 1) {
                yield return textBubble.Display("Gravedigger", "I'm on a roll!!");
            }
            else {
                yield return textBubble.Display("Gravedigger", "This is incredible!");
            }

            if (Random.Range(0, 2) < 1) {
                yield return textBubble.Display("Gravedigger", "Look at me go!");
            }
            else {
                yield return textBubble.Display("Gravedigger", "I haven't felt this young since I was 80!");
            }

                
        }
        else if (Inventory.Contains("Jackhammer")) {
            yield return textBubble.Display("Gravedigger", "WOAH");
            yield return textBubble.Display("Gravedigger", "What is this???");
            yield return textBubble.Display("Gravedigger", "It's like a shovel on steroids or something??");
            yield return textBubble.Display("Gravedigger", "Here, take my old shovel. I wanna give 'er a whirl!");

            shovelProp.SetActive(false);
            jackhammerProp.SetActive(true);

            Inventory.RemoveItem("Jackhammer");
            Inventory.AddItem("Shovel");
            log.Add("giveJackhammer");
            jackhammer = true;
            defaultSpeed = 2f;
        }
        else if (log.Contains("talkedToGravedigger")) {
            yield return textBubble.Display("Gravedigger", "Hey, did you find me something better to dig with?");
            yield return textBubble.Display("Gravedigger", "If you do, I will let you borrow my old shovel.");
            yield return textBubble.Display("Gravedigger", "...");
            yield return textBubble.Display("Gravedigger", "It's super cold. Make sure you are wrapped up nice and warm...");
        }
        else {
            yield return textBubble.Display("Gravedigger", "It's a damn cold night, isn't it?");
            yield return textBubble.Display("Gravedigger", "I've been doing this job for years...");
            yield return textBubble.Display("Gravedigger", "dug more graves than I can count...");
            yield return textBubble.Display("Gravedigger", "...");
            yield return textBubble.Display("Gravedigger", "Y'know, my shovel's looking a little worn after all these years.");
            yield return textBubble.Display("Gravedigger", "I could do with an upgrade.");
            yield return textBubble.Display("Gravedigger", "If you find anything that could replace my shovel, let me know!");

            log.Add("talkedToGravedigger");
        }




        hopping = true;

        while (Vector3.Dot(transform.forward, prevDir) < 0.999f) {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, prevDir, Mathf.PI * 0.5f * Time.deltaTime, 0f));
            yield return null;
        }

        speed = defaultSpeed;
        EnablePlayer();
    }

    IEnumerator Patrol() {
        while (true) {
            foreach (Transform patrolPoint in patrolPoints) {
                Vector3 target = patrolPoint.position;

                Vector3 dir = (target - transform.position).normalized;

                while (Vector3.Dot(transform.forward, dir) < 0.999f) {
                    transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, dir, Mathf.PI * 0.5f * Time.deltaTime * speed, 0f));
                    yield return null;
                }

                while (Vector3.Distance(transform.position, target) > 0.5f) {
                    transform.Translate(dir * Time.deltaTime * speed, Space.World);
                    yield return null;
                }

            }
        }
    }
}

/*
yield return textBubble.Display("Gravedigger", "Every second that passes, a person will die...");
yield return textBubble.Display("Gravedigger", "Each one a story, who's pen hath run dry.");
yield return textBubble.Display("Gravedigger", "While their stories are different, their endings are not -");
yield return textBubble.Display("Gravedigger", "Buried six feet under, and left there to rot.");
yield return textBubble.Display("Gravedigger", "Thousands of stories my shovel has seen,");
yield return textBubble.Display("Gravedigger", "Though my shovel cares not to distinguish between,");
yield return textBubble.Display("Gravedigger", "Accountant or lawyer, policeman or maid,");
yield return textBubble.Display("Gravedigger", "All are the same in the eyes of my spade.");
yield return textBubble.Display("Gravedigger", "Now I've been at it for years, my joints move with a creak,");
yield return textBubble.Display("Gravedigger", "My skin's turning yellow, and my bones are quite weak.");
yield return textBubble.Display("Gravedigger", "But when my own time is up and I lay stiff as can be,");
yield return textBubble.Display("Gravedigger", "Who's shovel will be burying me?");
*/