using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playervelocity;
    public float speed = 5f;
    private bool IsGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public bool lerpCrouch;
    public bool crouching = false;
    public bool sprinting = false;
    public float crouchTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer = -Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            float baseheight = 1.768f;
            float crouchheight = 0.7f;
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, baseheight, 15);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, crouchheight, 15);
            }
            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    //Receives inputs and applies them to controller
    public void ProcesssMove(Vector2 input)
    {
        Vector3 movementDirection = Vector3.zero;
        movementDirection.x = input.x;
        movementDirection.z = input.y;
        controller.Move(transform.TransformDirection(movementDirection) * speed * Time.deltaTime);
        playervelocity.y += gravity * Time.deltaTime;
        if(IsGrounded && playervelocity.y < 0)
        {
            playervelocity.y = -2f;
        }
        controller.Move(playervelocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (IsGrounded)
        {
            playervelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
        if (crouching)
            speed = 3f;
        else
            speed = 5f;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = 8;
        }
        else
        {
            speed = 5;
        }
    }
}
