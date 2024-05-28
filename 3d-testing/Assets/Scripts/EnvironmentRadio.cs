using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentRadio : MonoBehaviour, IInteractible
{
    bool activated = false;

    public AudioSource radio;
    public AudioClip song;

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
        radio.volume = 0.75f * AudioManagement.gameVolume;
    }

    void Update()
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
