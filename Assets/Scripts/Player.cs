using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = -5f;
    public float jumpStrength = 1.6f;
    float yVelocity;

    public AudioSource jump;
    public AudioSource land;

    float vx;
    float vy;
    float vz;
    public float moveForce = 10f;

    bool wasGroundedLastFrame;

    public bool control = true;

    CharacterController controller;

    void Awake() {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (control && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            vx = Mathf.Max(vx - Time.deltaTime * moveForce, -1f);
        }
        else if (control && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
            vx = Mathf.Min(vx + Time.deltaTime * moveForce, 1f);
        }
        else {
            if (vx > 0f) {
                vx = Mathf.Max(vx - Time.deltaTime * moveForce, 0f);
            }
            else if (vx < 0f) {
                vx = Mathf.Min(vx + Time.deltaTime * moveForce, 0f);
            }
        }

        if (control && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) {
            vz = Mathf.Max(vz - Time.deltaTime * moveForce, -1f);
        }
        else if (control && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
            vz = Mathf.Min(vz + Time.deltaTime * moveForce, 1f);
        }
        else {
            if (vz > 0f) {
                vz = Mathf.Max(vz - Time.deltaTime * moveForce, 0f);
            }
            else if (vz < 0f) {
                vz = Mathf.Min(vz + Time.deltaTime * moveForce, 0f);
            }
        }

        if (control && controller.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            //jump.Play();
            vy = jumpStrength;
        }
        else if (!controller.isGrounded && wasGroundedLastFrame && vy <= 0f) {
            vy = 0f;
        }
        else if (controller.isGrounded && !wasGroundedLastFrame) {
            //land.Play();
        }
        wasGroundedLastFrame = controller.isGrounded;


        Vector3 move = transform.right * vx + transform.forward * vz;

        if (Mathf.Sqrt((move.x * move.x) + (move.z * move.z)) > 1f) {
            move.Normalize();
        }

        if (controller.isGrounded && vy <= 0f) {
            vy = -10f;
        }
        else {
            vy += gravity * Time.deltaTime;
        }

        move += Vector3.up * vy;

        controller.Move(move * speed * Time.deltaTime);
    }
}
