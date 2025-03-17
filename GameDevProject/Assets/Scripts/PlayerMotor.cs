using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    
    private CharacterController controller;
    private Vector3 playerVelocity;
    
    
    private bool isGrounded;
    private bool sprinting;
    private bool lerpcrouch;
    private float speed;
    private bool crouching;
    private bool prone;

    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    
    private float walkSpeed = 4f;
    private float sprintSpeed = 7f;
    private float crouchSpeed = 2.5f;
    private float proneSpeed = 1.3f;

    private float normalHeight = 2f;
    private float crouchHeight = 1.2f;
    private float proneHeight = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        
    }
    public void processMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        
    }
    public void Jump()
    {
        
        if(isGrounded && !prone)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        else if (prone)
        {
            prone = true;
        }
        
    }
    public void SetSprinting(bool isSprinting)
    {
        if (isGrounded && !prone && !crouching)
        {
            sprinting = isSprinting;
            speed = sprinting ? sprintSpeed : walkSpeed;
        }
        
    }
    public void ToggleCrouch()
    {
        if (isGrounded)
        {
            crouching = !crouching;
            speed = crouching ? crouchSpeed : walkSpeed;
            controller.height = crouching ? crouchHeight : normalHeight;
        }
    }

    public void ToggleProne()
    {
        
            if (prone)
            {
                // If prone, stand up fully
                prone = false;
                crouching = false;
                speed = walkSpeed;
                controller.height = normalHeight;
            }
            else
            {
                // If standing or crouching, go prone
                prone = true;
                crouching = false;
                speed = proneSpeed;
                controller.height = proneHeight;
            }
        
    }
}
