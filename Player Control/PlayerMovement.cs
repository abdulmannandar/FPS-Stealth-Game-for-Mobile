using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    public float SpeedMove = 5f;
    private CharacterController controller;

    public float jumprate = 3f;
    private float nexttimetojump = 0f;

    private float Gravity = -20.81f;
    public float GroundDistance = 0.3f;
    public Transform Ground;
    public LayerMask layermask;
    Vector3 velocity;
    public float jumpheight = 2f;
    private float crouchSpeed = 3f;
    private float proneSpeed = 1f;

    private Vector3 crouchPose = new Vector3(0, 0.5f, 0);
    private Vector3 pronePose = new Vector3(0, 0.9f, 0); 

    public Transform camero;
    private Vector3 originalCameraPosition;

    [Header("ButtonAssign")]
    public GameObject crouchButton;
    public GameObject proneButton;
    public GameObject stand1Button;
    public GameObject stand2Button;

    public bool isGround;
    public bool isCrouched = false;
    public bool isProned = false;
     
    void Start()
    {
        originalCameraPosition = camero.localPosition;
        controller = GetComponent<CharacterController>(); 
    }
    void Update()
    {
        isGround = Physics.CheckSphere(Ground.position, GroundDistance, layermask);
        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        controller.Move(Move * SpeedMove * Time.deltaTime); 

        velocity.y += Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); 
    }
    public void Jump()
    {
        if (Time.time >= nexttimetojump)
        {
            nexttimetojump = Time.time + 1f / jumprate;
            if (isGround)
            {
                if (camero.localPosition == originalCameraPosition)
                {
                    velocity.y = Mathf.Sqrt(jumpheight * -2f * Gravity);
                    isGround = false;
                }
            }
        }
    }
    public void Crouch()
    {
        camero.localPosition = originalCameraPosition - crouchPose;
        SpeedMove = crouchSpeed;
        crouchButton.SetActive(false);
        stand1Button.SetActive(true);
        stand2Button.SetActive(false);
        proneButton.SetActive(true);
        isCrouched = true;
        isProned = false;
    }
    public void Prone()
    {
        camero.localPosition = originalCameraPosition - pronePose;
        SpeedMove = proneSpeed;
        crouchButton.SetActive(true);
        stand1Button.SetActive(false);
        stand2Button.SetActive(true);
        proneButton.SetActive(false);
        isCrouched = true;
        isProned = true;
    }
    public void Stand1()
    {
        camero.localPosition = originalCameraPosition;
        SpeedMove = 5f;
        crouchButton.SetActive(true);
        stand1Button.SetActive(false);
        stand2Button.SetActive(false);
        proneButton.SetActive(true);
        isCrouched = false;
        isProned = false;
    }
    public void Stand2()
    {
        camero.localPosition = originalCameraPosition;
        SpeedMove = 5f;
        crouchButton.SetActive(true);
        stand1Button.SetActive(false);
        stand2Button.SetActive(false);
        proneButton.SetActive(true);
        isCrouched = false;
        isProned = false;
    }
}
