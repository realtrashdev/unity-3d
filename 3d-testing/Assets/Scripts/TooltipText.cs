using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TooltipText : MonoBehaviour
{
    TMP_Text tooltip;
    Color textColor;

    float timer = 0;

    void Start()
    {
        tooltip = gameObject.GetComponent<TMP_Text>();
    }

    public void Tooltip(string text, Color color, float length)
    {
        tooltip.text = text;
        textColor = color;
        timer = length;
        tooltip.color = Color.clear;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Tooltip("this is a test", Color.white, 5);
        }

        if (timer > 0)
        {
            tooltip.color = Color.Lerp(tooltip.color, textColor, 3 * Time.deltaTime);
            timer -= Time.deltaTime;
        }

        else
        {
            tooltip.color = Color.Lerp(tooltip.color, Color.clear, 3 * Time.deltaTime);
        }
    }
}
