using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Animator camAnim;
    public Animator playerAnim;
    public Collider crouchDetection;

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float maxStamina;
    public float stamina;
    public float staminaDelay;
    [HideInInspector] public bool crouched = false;
    private bool canUncrouch = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    [HideInInspector] public bool isGrounded;

    [Header("Stamina")]
    public StaminaBar staminaBar;
    public GameObject staminaBarUI;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    public AudioClip[] grassSteps;
    private AudioClip grassStepClip;
    private float audioTimer;

    public Transform orientation;

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

        CheckCrouch();
        CheckSprint();

        GetInput();
        SpeedControl();
        MovementAudio();

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
        if (stamina >= maxStamina)
        {
            if (stamina > maxStamina) { stamina = maxStamina; }
            staminaBarUI.GetComponent<Image>().color = Color.Lerp(staminaBarUI.GetComponent<Image>().color, new Color(0f, 0f, 0f, 0f), Time.deltaTime * 7.5f);
        }

        //stamina goes negative, set it to 0
        else if (stamina < 0)
        {
            stamina = 0;
        }

        //if player fully runs out of stamina, delay stamina regen for slightly longer
        if (stamina == 0)
        {
            staminaDelay = 2f;
        }


        //if: holding shift, moving, while not having a stamina delay, and not crouching, ramp up movement speed to running (4) and decrease stamina.
        if (Input.GetKey(KeyCode.LeftShift) && staminaDelay <= 0 && rb.velocity != Vector3.zero && !crouched)
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

        //if not sprinting or not moving, set speed to walking (2) and check if stamina delay is active. if not, regain stamina.
        else if (!Input.GetKey(KeyCode.LeftShift) || rb.velocity == Vector3.zero || crouched)
        {
            if (stamina == 0)
            {
                staminaDelay = 2f;
                stamina = 0.001f;
            }

            else if (staminaDelay <= 0 && stamina < maxStamina)
            {
                Debug.Log("Refilling Stamina...");
                stamina += Time.deltaTime;
            }

            else
            {
                staminaDelay -= Time.deltaTime;
            }

            //if not crouching, set movement speed to walking (2)
            if (!crouched)
            {
                moveSpeed = 2;
            }
        }

        //if somehow none of these if statements get called, default to walking speed.
        else if (!crouched)
        {
            moveSpeed = 2;
        }

        //update stamina bar
        staminaBar.SetStamina(stamina);
    }

    void CheckCrouch()
    {
        //if holding left control and not already crouching, crouched = true
        if (Input.GetKey(KeyCode.LeftControl))
        {
            crouched = true;
            crouchDetection.enabled = true;
        }

        else if (canUncrouch) 
        {
            crouched = false;
            crouchDetection.enabled = false;
        }

        camAnim.SetBool("crouching", crouched);
        playerAnim.SetBool("crouching", crouched);

        //lower movement speed to crouching (1) and set speed to crouching if it gets too low
        if (crouched && moveSpeed != 1)
        {
            moveSpeed -= moveSpeed/10;
            
            if (moveSpeed < 1)
            {
                moveSpeed = 1;
            }
        }
    }

    void MovementAudio()
    {
        if (!crouched && rb.velocity != Vector3.zero)
        {
            //grass specific, change later when adding more materials
            int index = Random.Range(0, grassSteps.Length);
            grassStepClip = grassSteps[index];

            if (audioTimer <= 0)
            {
                audioSource.pitch = Random.Range(1f, 1.2f);
                audioSource.PlayOneShot(grassStepClip);
            }

            if (moveSpeed == 2 && audioTimer <= 0)
            {
                audioTimer = 0.6f;
            }

            else if (moveSpeed > 2 && audioTimer <= 0)
            {
                audioTimer = 0.3f;
            }

            audioTimer -= Time.deltaTime;
        }
    }

    //while crouching, detect if there is something above the player that would block them from standing
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("canCrouchUnder"))
        {
            Debug.Log("Entered Trigger");
            canUncrouch = false;
        }
    }

    //detect when the area above the player is no longer blocked then and allows them to stand
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("canCrouchUnder"))
        {
            Debug.Log("Exited Trigger");
            canUncrouch = true;
        }
    }
}