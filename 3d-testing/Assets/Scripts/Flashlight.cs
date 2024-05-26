using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Flashlight : MonoBehaviour
{
    bool lightOn = true;
    [SerializeField] private GameObject spotLight;
    [SerializeField] private Animator anim;
    [SerializeField] private FlashlightSway flashlightSway;

    void Start()
    {
        spotLight = GameObject.Find("FlashlightLight");
    }

    void Update()
    {
        CheckForLight();
    }

    void CheckForLight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            lightOn = !lightOn;
            flashlightSway.doSway = lightOn;
            spotLight.SetActive(lightOn);
            anim.SetBool("lightOn", lightOn);
        }
    }
}
