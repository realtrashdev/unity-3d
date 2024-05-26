using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] public float smooth;
    [SerializeField] private float swayMultiplier;

    [HideInInspector] public bool doSway = true;

    public Transform playerCam;

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

        //move & rotate
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(-0.48f, -0.77f, 0.3f), smooth * Time.deltaTime);
    }
}
