using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lighter : MonoBehaviour
{
    bool lightOn = false;
    bool turningOff = false;

    int flicks;
    int minFlicks = 1;
    int maxFlicks = 3;
    int flickModifier = -1;

    float timer;
    float fuel = 180;

    string handed;

    [SerializeField] private GameObject flame;
    [SerializeField] private GameObject flameImage;
    [SerializeField] private FlashlightSway sway;

    [Header("Animation + Audio")]
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip lighterOpen;
    [SerializeField] private AudioClip lighterFlick;
    [SerializeField] private AudioClip lighterClose;
    [SerializeField] bool leftHanded;
    [SerializeField] GameObject brokenLighter;

    [Header("User Interface")]
    [SerializeField] private LighterFuelBar fuelBar;
    [SerializeField] private GameObject fuelBarUI;
    [SerializeField] private TooltipText tooltip;

    void Start()
    {
        fuelBar.SetMaxLighterFuel(fuel);
        anim.SetBool("leftHanded", leftHanded);
        audioSource.volume *= AudioManagement.gameVolume;
    }

    void OnEnable()
    {
        CheckHands();
        fuelBar.SetLighterFuel(fuel);

        flicks = Random.Range(minFlicks + flickModifier, maxFlicks + flickModifier);
        Debug.Log(flicks);
        timer = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            fuel = 1;
        }

        if (fuel > 0)
        {
            CheckForLight();
            DecreaseFuel();

            timer -= Time.deltaTime;
        }

        else
        {
            if (!turningOff)
            {
                tooltip.Tooltip("Your lighter ran out of fuel.", Color.white, 5);
                anim.Play("Lighter_Close_" + handed);
                timer = 1;
                turningOff = true;
            }

            if (timer <= 0)
            {
                Destroy(gameObject);
            }

            Debug.Log(timer);
            timer -= Time.deltaTime;
        }
    }

    void CheckForLight()
    {
        if (Input.GetKeyDown(KeyCode.F) && timer <= 0)
        {
            if (flicks > 5 && !lightOn)
            {
                flicks--;
                audioSource.clip = lighterOpen;
                anim.Play("Lighter_Open_" + handed);
                flicks = Random.Range(minFlicks + flickModifier, maxFlicks + flickModifier);
                timer = 1;
            }

            else if (flicks > 0 && !lightOn)
            {
                flicks--;
                audioSource.PlayOneShot(lighterFlick);
                anim.Play("Lighter_Flick_" + handed);
                timer = 0.5f;
            }

            else if (flicks == 0 && !lightOn)
            {
                flicks--;
                audioSource.PlayOneShot(lighterFlick);
                anim.Play("Lighter_FlickWin_" + handed);
                lightOn = true;
                timer = 2;
            }

            else if (lightOn)
            {
                flicks--;
                audioSource.clip = lighterClose;
                anim.Play("Lighter_Close_" + handed);
                lightOn = false;
                flicks = 10;
                timer = 1;
            }

            anim.SetBool("lightOn", lightOn);
        }
    }

    void DecreaseFuel()
    {
        if (lightOn)
        {
            fuel -= Time.deltaTime;
            fuelBar.SetLighterFuel(fuel);
            fuelBarUI.GetComponent<Image>().color = Color.Lerp(fuelBarUI.GetComponent<Image>().color, new Color(1f, 1f, 1f, 0.5f), Time.deltaTime * 15);
        }

        else
        {
            fuelBarUI.GetComponent<Image>().color = Color.Lerp(fuelBarUI.GetComponent<Image>().color, new Color(0, 0, 0, 0), Time.deltaTime * 15);
        }

        if (fuel < 45 && flickModifier < 2)
        {
            Debug.Log("Flick Modifier Updated");
            flickModifier = 2;
        }

        else if (fuel < 90 && flickModifier < 1)
        {
            Debug.Log("Flick Modifier Updated");
            flickModifier = 1;
        }

        else if (fuel < 135 && flickModifier < 0)
        {
            Debug.Log("Flick Modifier Updated");
            flickModifier = 0;
        }
    }

    void CheckHands()
    {
        if (leftHanded)
        {
            handed = "LeftHanded";
        }

        else
        {
            handed = "RightHanded";
        }
    }
}