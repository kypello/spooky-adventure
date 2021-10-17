using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravedigger : Interactable
{
    public Transform[] patrolPoints;

    public float speed = 1f;

    public override IEnumerator Interact() {
        yield break;
    }

    void Start() {
        StartCoroutine(Patrol());
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
                    transform.Translate(dir * Time.deltaTime * speed);
                    yield return null;
                }

            }
        }
    }
}
