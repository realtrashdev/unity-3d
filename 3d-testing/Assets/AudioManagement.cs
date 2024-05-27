using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    public static float gameVolume = 1;

    public AudioSource ambientAudioSource;
    public float ambientDefaultVolume;


    private void Start()
    {
        ambientAudioSource.volume *= gameVolume;
    }

    void Update()
    {
        if (PauseMenu.gamePaused)
        {
            ambientAudioSource.volume = ambientDefaultVolume / 2;
        }

        else
        {
            ambientAudioSource.volume = ambientDefaultVolume * gameVolume;
        }
    }
}
