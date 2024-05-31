using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour, IInteractible
{
    public DoorBlocker doorBlocker;
    [SerializeField] public bool opened;
    private string openedText = "Closed";

    [SerializeField] private Animator anim;

    [Header("Interaction")]
    public Interactor interactor;
    public TMP_Text interactPrompt;

    public void Interact()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Door_Idle_" + openedText) && !doorBlocker.blocking)
        {
            if (opened)
            {
                anim.Play("Door_Close");
            }

            else
            {
                anim.Play("Door_Open");
            }

            opened = !opened;
            SetOpenedText();
        }
    }

    public void SetInteractPrompt()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Door_Idle_" + openedText))
        {
            if (opened && !doorBlocker.blocking)
            {
                interactPrompt.text = "Close door: [E]";
            }

            else if (!opened && !doorBlocker.blocking)
            {
                interactPrompt.text = "Open door: [E]";
            }

            else
            {
                interactPrompt.text = "Blocked";
            }
        }

        else
        {
            interactPrompt.text = "";
        }
    }

    void SetOpenedText()
    {
        if (opened)
        {
            openedText = "Open";
        }

        else
        {
            openedText = "Closed";
        }
    }
}
