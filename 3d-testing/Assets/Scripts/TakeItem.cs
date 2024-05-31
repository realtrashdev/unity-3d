using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class TakeItem : MonoBehaviour, IInteractible
{
    [Header("Item Name (lowercase)")]
    public GameObject item;

    [Header("References")]
    [SerializeField] private TMP_Text interactPrompt;

    public void SetInteractPrompt()
    {
        Debug.Log("Setting Interact Prompt.");
        interactPrompt.text = "Take " + item.name + ": [E]";
    }

    public void Interact()
    {
        Debug.Log("Took " + item.name + ".");
        ItemManagement.inventory.Add(item);
        item.SetActive(true);
        Destroy(gameObject);
    }
}
