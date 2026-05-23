using CameraDoorScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feetsounds : MonoBehaviour
{
    public FixedJoystick joystick;
    public AudioClip footstepSound;
    public AudioClip footstepSound2;
    public AudioClip jumpSound;
    public AudioClip landSound;
    private AudioSource audioSource;
    private CharacterController characterController;
    private bool isGrounded;
    public float stepInterval = 0.5f;
    private float stepTimer = 0f;
    private bool useFirstClip = true;
    public Transform camero;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
        isGrounded = characterController.isGrounded;
    }

    void Update()
    {
        // Play footstep sounds
        if (IsMoving() && characterController.isGrounded && IsCorrectScale())
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
        // Check if the character has jumped
        if (!isGrounded && characterController.isGrounded)
        {
            isGrounded = true;
            audioSource.clip = landSound;
            audioSource.Play();
        }
        else if (isGrounded && !characterController.isGrounded)
        {
            isGrounded = false;
            audioSource.clip = jumpSound;
            audioSource.Play();
        }
    }
    bool IsMoving()
    {
        Vector3 move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        return move.magnitude > 0.1f;
    }
    void PlayFootstep()
    {
        if (useFirstClip)
        {
            audioSource.clip = footstepSound;
        }
        else
        {
            audioSource.clip = footstepSound2;
        }

        audioSource.Play();
        useFirstClip = !useFirstClip;
    }
    bool IsCorrectScale()
    { 
        return camero.localPosition == new Vector3(0, 0.737f, 0.147f);
    }
}


