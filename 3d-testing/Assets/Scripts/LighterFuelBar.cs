using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LighterFuelBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxLighterFuel(float fuel)
    {
        slider.maxValue = fuel;
        slider.value = fuel;
    }

    public void SetLighterFuel(float fuel)
    {
        slider.value = fuel;
    }
}
