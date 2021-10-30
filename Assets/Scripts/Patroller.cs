using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : Interactable
{
    public Transform patrol;
    Transform[] patrolPoints;

    protected float speed;
    float rotateSpeed;

    public float defaultSpeed = 1f;
    public float defaultRotateSpeed = 1;

    protected bool hopping = true;
    bool notMoving = false;

    bool hasAnimation;
    bool hasParticles;
    bool hasFootsteps;

    public int startNode;
    public bool teleportOnEnd;

    [System.Serializable]
    public struct PatrolVisuals {
        public bool hasAnim;

        public bool hasParticles;

        public Animation anim;
        public ParticleSystem particles;
        public AudioSource[] footsteps;

        public bool hasAltCycle;
        public float altCycleTimeLength;
    }
    public PatrolVisuals visuals;

    bool onAltCycle = false;
    float timer;

    Vector3 targetCC;
    float targetCR;
    Vector3 turnCC;
    float turnCR;
    Vector3 pointA;
    Vector3 pointB;
    Vector3 prev;
    Vector3 next;

    public Vector3 lookOffset;

    void Start() {

        timer = 0f;
        
        StartCoroutine(Patrol());
    }

    void Update() {
        if (onAltCycle) {
            timer -= Time.deltaTime;
        }

        if ((hopping || onAltCycle) && visuals.hasAnim && (!visuals.anim.isPlaying || (onAltCycle && timer <= 0f))) {

            visuals.anim.Play();

            if (hasParticles) {
                visuals.particles.Play();
            }
            
            visuals.footsteps[Random.Range(0, visuals.footsteps.Length)].Play();

            if (visuals.hasAltCycle) {
                onAltCycle = !onAltCycle;
                timer = visuals.altCycleTimeLength;
            }
        }
    }

    public override IEnumerator Interact() {
        speed = 0f;
        rotateSpeed = 0f;
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(transform.position + lookOffset));

        Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 dirToPlayer = (playerPos - transform.position).normalized;

        Vector3 prevDir = transform.forward;

        notMoving = true;

        while (Vector3.Dot(transform.forward, dirToPlayer) < 0.9999f) {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, dirToPlayer, Mathf.PI * Time.deltaTime, 0f));
            yield return null;
        }

        hopping = false;

        yield return StopAndChat();

        hopping = true;

        while (Vector3.Dot(transform.forward, prevDir) < 0.9999f) {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, prevDir, Mathf.PI * Time.deltaTime, 0f));
            yield return null;
        }

        notMoving = false;

        speed = defaultSpeed;
        rotateSpeed = defaultRotateSpeed;
        EnablePlayer();
    }

    public virtual IEnumerator StopAndChat() {
        yield break;
    }

    IEnumerator Patrol() {
        patrolPoints = patrol.GetComponentsInChildren<Transform>();
        speed = defaultSpeed;
        rotateSpeed = defaultRotateSpeed;

        int i = startNode;
        transform.position = patrolPoints[i].position;

        i++;
        if (i >= patrolPoints.Length) {
            i = 0;
        }

        transform.rotation = Quaternion.LookRotation(patrolPoints[i].position - transform.position);

        while (true) {
            while (i < patrolPoints.Length) {
                if (i == 0 && teleportOnEnd) {
                    transform.position = patrolPoints[0].position;
                    transform.rotation = Quaternion.LookRotation(patrolPoints[1].position - transform.position);

                    i++;
                    continue;
                }
                Vector3 target = patrolPoints[i].position;

                Vector3 nextTarget;
                if (i == patrolPoints.Length - 1) {
                    nextTarget = patrolPoints[0].position;
                }
                else {
                    nextTarget = patrolPoints[i + 1].position;
                }

                float timeToTurnInCircle = (Mathf.PI * 2f) / rotateSpeed;
                float turnCircleCircumference = defaultSpeed * timeToTurnInCircle;
                float turnCircleRadius = turnCircleCircumference / (Mathf.PI * 2f);

                Vector3 dirToPrev = (transform.position - target).normalized;
                Vector3 dirToNext = (nextTarget - target).normalized;
                float angleToTurnCircleCenter = (Vector3.Angle(dirToPrev, dirToNext) * Mathf.Deg2Rad) / 2f;

                float targetCircleRadius = turnCircleRadius / Mathf.Tan(angleToTurnCircleCenter);

                Vector3 dirToTurnCircleCenter = ((dirToPrev + dirToNext) / 2f).normalized;
                float distanceToTurnCircleCenter = turnCircleRadius / Mathf.Sin(angleToTurnCircleCenter);

                targetCC = target;
                targetCR = targetCircleRadius;
                turnCC = target + dirToTurnCircleCenter * distanceToTurnCircleCenter;
                turnCR = turnCircleRadius;
                pointA = target + dirToPrev * targetCircleRadius;
                pointB = target + dirToNext * targetCircleRadius;
                
                if (i == 0) {
                    prev = (patrolPoints[patrolPoints.Length - 1].position);
                }
                else {
                    prev = patrolPoints[i - 1].position;
                }
                next = nextTarget;

                while (Vector3.Distance(transform.position, target) > targetCircleRadius) {
                    transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
                    yield return null;
                }

                while (Vector3.Dot(transform.forward, dirToNext) < 0.9999f || notMoving) {
                    transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, dirToNext, rotateSpeed * Time.deltaTime, 0f));
                    transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
                    yield return null;
                }
                transform.rotation = Quaternion.LookRotation(dirToNext);
                //transform.position = target + dirToNext * targetCircleRadius;

                i++;
            }
            i = 0;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(targetCC, targetCR);
        Gizmos.DrawWireSphere(turnCC, turnCR);
        Gizmos.DrawSphere(pointA, 0.1f);
        Gizmos.DrawSphere(pointB, 0.1f);
        Gizmos.DrawLine(prev, pointA);
        Gizmos.DrawLine(pointB, next);
    }
}
