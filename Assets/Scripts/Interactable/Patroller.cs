using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : Interactable
{
    public Transform[] patrolPoints;

    protected float speed;

    public float defaultSpeed = 1f;

    protected bool hopping = true;
    public Animation anim;

    void Start() {
        StartCoroutine(Patrol());
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

        yield return StopAndChat();




        hopping = true;

        while (Vector3.Dot(transform.forward, prevDir) < 0.999f) {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, prevDir, Mathf.PI * 0.5f * Time.deltaTime, 0f));
            yield return null;
        }

        speed = defaultSpeed;
        EnablePlayer();
    }

    public virtual IEnumerator StopAndChat() {
        yield break;
    }

    IEnumerator Patrol() {
        speed = defaultSpeed;

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
