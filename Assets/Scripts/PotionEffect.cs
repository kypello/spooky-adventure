using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PotionEffect : MonoBehaviour
{
    public int pointCount = 5;
    public float speed = 1f;

    public Material mat;

    Vector4[] points;

    struct PointInfo {
        public float angle;
        public float velocity;
        public float angularVelocity;

        public void Randomize() {
            angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            velocity = Random.Range(0.05f, 0.2f);
            angularVelocity = Random.Range(-1f, 1f);
        }

        public void Update(float s) {
            angle += angularVelocity * Time.deltaTime * s;
            if (angle >= Mathf.PI * 2f) {
                angle -= Mathf.PI * 2f;
            }
            else if (angle < 0f) {
                angle += Mathf.PI * 2f;
            }
        }

        public Vector4 GetDir() {
            return new Vector4(Mathf.Cos(angle), Mathf.Sin(angle), 0f, 0f);
        }
    }
    PointInfo[] pointInfos;

    static Vector2[] offsets = new Vector2[] {new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1),
        new Vector2(-1, 0), new Vector2(1, 0),
        new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1) };

    void OnEnable() {
        points = new Vector4[pointCount * 9];
        pointInfos = new PointInfo[pointCount];

        for (int i = 0; i < pointCount; i++) {
            points[i] = new Vector4(Random.Range(0f, 1f), Random.Range(0f, 1f), 0f, 0f);
            pointInfos[i].Randomize();
        }

        SetEdgePointClones();
    }

    void SetEdgePointClones() {
        for (int i = 1; i < 9; i++) {
            for (int j = 0; j < pointCount; j++) {
                points[pointCount * i + j] = points[j] + (Vector4)offsets[i - 1];
            }
        }
    }

    void Update() {
        for (int i = 0; i < pointCount; i++) {
            pointInfos[i].Update(speed);
            points[i] += pointInfos[i].GetDir() * pointInfos[i].velocity * Time.deltaTime * speed;

            if (points[i].x < 0f) {
                points[i] += new Vector4(1f, 0f, 0f, 0f);
            }
            else if (points[i].x >= 1f) {
                points[i] += new Vector4(-1f, 0f, 0f, 0f);
            }

            if (points[i].y < 0f) {
                points[i] += new Vector4(0f, 1f, 0f, 0f);
            }
            else if (points[i].y >= 1f) {
                points[i] += new Vector4(0f, -1f, 0f, 0f);
            }
        }

        SetEdgePointClones();

        mat.SetVectorArray("_Points", points);
    }
}
