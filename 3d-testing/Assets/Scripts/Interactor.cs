using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractible
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public GameObject interactPrompt;
    public Transform interactorSource;
    public float interactRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
            Ray r = new Ray(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractible interactObj))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactObj.Interact();
                    }

                    Debug.Log("Interactible Found");
                    interactPrompt.SetActive(true);
                }
            }

            else
        {
            interactPrompt.SetActive(false);
        }
        //}
    }
}
