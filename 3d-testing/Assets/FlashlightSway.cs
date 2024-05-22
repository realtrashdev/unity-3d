using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] public float smooth;
    [SerializeField] private float swayMultiplier;

    [HideInInspector] public bool doSway = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doSway)
        {
            Sway();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            doSway = !doSway;
        }
    }

    void Sway()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        //calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        //rotate
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
