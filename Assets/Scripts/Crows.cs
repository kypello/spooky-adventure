using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crows : MonoBehaviour
{
    public float flyRange;
    public float respawnRange;
    public float flyTime;
    public Transform player;

    public Transform[] crows;
    Animation[] anims;

    Vector3[] startPositions;

    int crowCount;

    bool flownAway = false;
    bool flying = false;

    struct FlyingCrowInfo {
        public Animation anim;
        public Transform crow;
        public Vector3 dir;
        public float speed;
        public float startDelay;

        public void Set(Transform c) {
            crow = c;
            dir = (new Vector3(Random.Range(-0.5f, 0.5f), 1f, Random.Range(-0.5f, 0.5f))).normalized;
            speed = Random.Range(2f, 3f);
            startDelay = Random.Range(0f, 0.4f);

        }

        public void Move() {
            crow.Translate(dir * speed * Time.deltaTime);
        }
    }

    void Awake() {
        crowCount = crows.Length;

        anims = new Animation[crowCount];
        startPositions = new Vector3[crowCount];

        for (int i = 0; i < crows.Length; i++) {
            anims[i] = crows[i].GetComponent<Animation>();
            startPositions[i] = crows[i].transform.position;
        }
    }

    void Update() {
        if (!flownAway && Vector3.Distance(transform.position, player.position) <= flyRange) {
            StartCoroutine(FlyAway());
        }
    }

    IEnumerator FlyAway() {
        flying = true;
        flownAway = true;

        foreach (Animation anim in anims) {
            anim.Play();
        }

        GetComponent<AudioSource>().Play();

        FlyingCrowInfo[] flyingInfos = new FlyingCrowInfo[crowCount];
        for (int i = 0; i < crowCount; i++) {
            flyingInfos[i] = new FlyingCrowInfo();
            flyingInfos[i].Set(crows[i]);
        }

        float time = flyTime;

        while (time > 0f) {
            time -= Time.deltaTime;

            for (int i = 0; i < crowCount; i++) {
                if (flyingInfos[i].startDelay <= 0f) {
                    flyingInfos[i].Move();
                }
                else {
                    flyingInfos[i].startDelay -= Time.deltaTime;
                }
            }

            yield return null;
        }

        flying = false;
    }
}
