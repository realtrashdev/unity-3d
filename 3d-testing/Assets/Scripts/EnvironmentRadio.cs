using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnvironmentRadio : MonoBehaviour, IInteractible
{
    bool activated = false;

    public AudioSource radio;
    public AudioClip song;

    //necessary for object interaction
    [Header("Interaction")]
    public Interactor interactor;
    public TMP_Text interactPrompt;

    public void SetInteractPrompt()
    {
        Debug.Log("Setting Interact Prompt.");

        if (activated)
        {
            interactPrompt.text = "Turn off: [E]";
        }

        else
        {
            interactPrompt.text = "Turn on: [E]";
        }
    }

    public void Interact()
    {
        Debug.Log("Interacted with Radio.");
        activated = !activated;

        if (activated)
        {
            radio.PlayOneShot(song);
        }

        if (!activated)
        {
            radio.Stop();
        }
    }

    void Start()
    {
        radio.volume = 0.4f * AudioManagement.gameVolume;
    }

    void Update()
    {
        CheckPause();
    }

    void CheckPause()
    {
        if (PauseMenu.gamePaused && radio.isPlaying)
        {
            radio.Pause();
        }

        else if (!PauseMenu.gamePaused && !radio.isPlaying)
        {
            radio.UnPause();

            if (!radio.isPlaying)
            {
                activated = false;
            }
        }
    }
}
