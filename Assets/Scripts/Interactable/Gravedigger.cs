using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravedigger : Interactable
{
    public Transform[] patrolPoints;

    public float speed = 1f;

    bool hopping = true;
    public Animation anim;

    void Start() {
        StartCoroutine(Patrol());
    }

    void Update() {
        if (hopping && !anim.isPlaying) {
            anim.Play();
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

        hopping = false;


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
        yield return textBubble.Display("Gravedigger", "...");
        yield return textBubble.Display("Gravedigger", "That's a little poem I wrote. It's about digging graves. What do you think of it?");




        hopping = true;

        while (Vector3.Dot(transform.forward, prevDir) < 0.999f) {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, prevDir, Mathf.PI * 0.5f * Time.deltaTime, 0f));
            yield return null;
        }

        speed = 1f;
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
