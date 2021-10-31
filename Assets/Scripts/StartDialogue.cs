using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : Interactable
{
    public Transform forwardTarget;

    bool holdingInvitation = true;
    bool invitationDropped = false;
    public Animation invitation;

    public GameObject candyCount;
    public GameObject candyIcon;

    public AudioSource mainMusic;

    public GameObject canvas;

    Collider collider;

    void Start() {
        collider = GetComponent<Collider>();
    }

    public override IEnumerator Interact() {
        DisablePlayer();
        StartCoroutine(playerLook.LookAt(forwardTarget.position));
        yield return textBubble.Display("", "Well, this looks like the place...");
        yield return textBubble.Display("", "Good thing I've got this incredibly convincing costume...");
        yield return textBubble.Display("", "...so no one will know that I'm actually a human!");
        yield return textBubble.Display("", "Let's get this party started!!");

        collider.enabled = false;

        EnablePlayer();

        candyCount.SetActive(true);
        candyIcon.SetActive(true);
        mainMusic.Play();

        canvas.SetActive(false);

        holdingInvitation = false;
        while (invitation.isPlaying) {
            yield return null;
        }
        invitation.Play("InvitationDropDown");
        yield return new WaitForSeconds(0.5f);
        invitation.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    void Update() {
        if (holdingInvitation && !invitation.isPlaying) {
            invitation.Play();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            StartCoroutine(Interact());
        }
    }
}
