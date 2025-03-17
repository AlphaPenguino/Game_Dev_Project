using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    
    private CharacterController controller;
    private Vector3 playerVelocity;
    
    private float speed = 5f;
    private bool isGrounded;
    private bool sprinting;
    private bool crouching;
    private bool lerpcrouch;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    
    private float fovSmoothSpeed = 5f;


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
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
    public void SetSprinting(bool isSprinting)
    {
        if (isGrounded)
        {
            sprinting = isSprinting;
            speed = sprinting ? 8f : 5f;
        }
        
    }
    public void SetCrouching(bool isCrouching)
    {
        
        
    }
}
