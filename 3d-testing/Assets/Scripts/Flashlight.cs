using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Flashlight : MonoBehaviour
{
    bool lightOn = true;
    GameObject spotLight;
    public Animator anim;

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
            //flashlightHolder.GetComponent<FlashlightSway>().doSway = lightOn;
            spotLight.SetActive(lightOn);
            anim.SetBool("lightOn", lightOn);
        }
    }
}
