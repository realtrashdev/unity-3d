using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Flashlight : MonoBehaviour
{
    bool lightOn = true;
    [SerializeField] private GameObject spotLight;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flashlightClick1;
    [SerializeField] private FlashlightSway flashlightSway;

    [SerializeField] bool leftHanded;

    void Start()
    {
        anim.SetBool("leftHanded", leftHanded);
        audioSource.volume *= AudioManagement.gameVolume;
    }

    void Update()
    {
        CheckForLight();
    }

    void CheckForLight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FlashlightSound();

            lightOn = !lightOn;
            flashlightSway.doSway = lightOn;
            spotLight.SetActive(lightOn);
            anim.SetBool("lightOn", lightOn);
        }
    }

    void FlashlightSound()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(flashlightClick1);
    }
}
