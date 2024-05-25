using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float maxStamina;
    public float stamina;
    public float staminaDelay;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    [HideInInspector] public bool isGrounded;

    public Transform orientation;

    [Header("Stamina")]
    public StaminaBar staminaBar;
    public GameObject staminaBarUI;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    void Start()
    {
        stamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = groundDrag;
    }

    // Update is called once per frame
    void Update()
    {
        //ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        CheckSprint();
        GetInput();
        SpeedControl();

        //apply drag accordingly
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        //calculate move direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce (moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    void SpeedControl()
    {
        //prevents player movement speed from going over the maximum
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void CheckSprint()
    {
        //if stamina exceeds the max, set it to the max & fade out stamina bar
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
            staminaBarUI.GetComponent<Image>().color = Color.Lerp(staminaBarUI.GetComponent<Image>().color, new Color(0f, 0f, 0f, 0), Time.deltaTime * 7.5f);
        }

        //stamina goes negative, set it to 0
        if (stamina < 0)
        {
            stamina = 0;
        }

        //if player fully runs out of stamina, delay stamina regen for slightly longer
        if (stamina == 0)
        {
            staminaDelay = 2f;
        }


        //if holding shift and moving while not having a stamina delay, ramp up movement speed to running and decrease stamina.
        if (Input.GetKey(KeyCode.LeftShift) && staminaDelay <= 0 && rb.velocity != Vector3.zero)
        {
            staminaBarUI.GetComponent<Image>().color = Color.Lerp(staminaBarUI.GetComponent<Image>().color, new Color(0.8f, 0.8f, 0.8f, 0.5f), Time.deltaTime * 15);

            if (moveSpeed != 4)
            {
                moveSpeed += 0.025f;

                if (moveSpeed > 4)
                {
                    moveSpeed = 4;
                }
            }

            stamina -= Time.deltaTime;
        }

        //if not holding shift or not moving, set speed to walking and check if stamina delay is active. if not, regain stamina.
        else if (!Input.GetKey(KeyCode.LeftShift) || rb.velocity == Vector3.zero)
        {
            if (stamina == 0)
            {
                staminaDelay = 2f;
                stamina = 0.001f;
            }

            if (staminaDelay <= 0)
            {
                stamina += Time.deltaTime;
            }

            else
            {
                staminaDelay -= Time.deltaTime;
            }

            moveSpeed = 2;
        }

        //if somehow neither of these if statements work, default to walking speed.
        else
        {
            moveSpeed = 2;
        }

        //update stamina bar
        staminaBar.SetStamina(stamina);
    }
}
