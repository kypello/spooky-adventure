using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{
    public Player player;
    public PlayerLook playerLook;
    public PlayerInteract playerInteract;

    public GameObject pauseScreen;

    public Slider slider;
    public TMP_Text sliderText;

    public bool paused;

    void Update() {
        if (!paused && player.control && Input.GetKeyDown(KeyCode.P)) {
            SetPausedState(true);
        }
        else if (paused) {
            playerLook.sensitivity = slider.value;

            SetSliderText();

            if (Input.GetKeyDown(KeyCode.P)) {
                SetPausedState(false);
            }
        }
    }

    void SetSliderText() {
        int percentage = Mathf.RoundToInt(((slider.value - slider.minValue) / (slider.maxValue - slider.minValue)) * 100f);

        sliderText.text = "<b>Sensitivity:</b> " + percentage + "%";
    }

    public void SetPausedState(bool p) {
        paused = p;
        player.control = !p;
        playerLook.control = !p;
        playerInteract.control = !p;

        pauseScreen.SetActive(p);

        if (p) {
            slider.value = playerLook.sensitivity;
            SetSliderText();
        }
    }
}
