using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusicQuietener : MonoBehaviour
{
    public AudioSource music;
    public Transform player;

    void Update() {
        music.volume = Mathf.Clamp(Vector3.Distance(transform.position, player.position) / 20f - 1f, 0f, 1f);
    }
}
