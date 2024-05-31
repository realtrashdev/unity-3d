using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DoorBlocker : MonoBehaviour, IInteractible
{
    public bool blocking = false;
    public Door door;
    private string blockingText = "Up";

    [SerializeField] Animator anim;
    [SerializeField] Animator doorAnim;

    [Header("Interaction")]
    public Interactor interactor;
    public TMP_Text interactPrompt;

    public void Interact()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorBlocker_Idle_" + blockingText) && doorAnim.GetCurrentAnimatorStateInfo(0).IsName("Door_Idle_Closed"))
        {
            if (!blocking && !door.opened)
            {
                blockingText = "Down";
                anim.Play("DoorBlocker_Drop");
            }

            else if (blocking && !door.opened)
            {
                blockingText = "Up";
                anim.Play("DoorBlocker_Lift");
            }
        }
    }

    public void SetInteractPrompt()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorBlocker_Idle_" + blockingText))
        {
            if (!blocking && !door.opened)
            {
                interactPrompt.text = "Pull down [E]";
            }

            else if (!blocking && door.opened)
            {
                interactPrompt.text = "Close door to pull down";
            }

            else if (blocking && !door.opened)
            {
                interactPrompt.text = "Pull up [E]";
            }

            else
            {
                interactPrompt.text = "This should not be showing";
            }
        }

        else
        {
            interactPrompt.text = "";
        }
    }
}