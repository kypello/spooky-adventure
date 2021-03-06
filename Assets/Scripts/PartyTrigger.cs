using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyTrigger : MonoBehaviour
{
    public Animation fade;
    public AudioSource partyMusic;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut() {
        fade.Play();
        yield return new WaitForSeconds(0.5f);
        DontDestroyOnLoad(partyMusic.gameObject);
        partyMusic.volume = 1f;
        SceneManager.LoadScene(1);
    }
}
