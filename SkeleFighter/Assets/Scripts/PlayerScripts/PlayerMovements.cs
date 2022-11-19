using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playervelocity;
    private Animator animation;
    private Enemy enemy;
    public float speed = 5f;
    private bool IsGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public bool lerpCrouch;
    public bool crouching = false;
    public bool sprinting = false;
    public float crouchTimer = 1f;
    public bool PlayerCrouch = false;
    public bool PlayerRun = false;
    public bool PlayerJump = false;
    public bool running;
    public bool CanAttack = true;
    public float AttackCooldown = 1f;
    public AudioClip punchSound;
    public bool isAttacking = false;
    InputAction movement;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animation = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
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
                animation.SetBool("Crouching", false);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, crouchheight, 15);
                animation.SetBool("Crouching", true);
            }
            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if ((Mathf.Abs(x) + Mathf.Abs(z)) > 0)
        {
            animation.SetBool("Running", true);
        }
        else if ((Mathf.Abs(x) + Mathf.Abs(z)) < 1)
        {
            animation.SetBool("Running", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                attack();
            }
        }
        Jump();
    }

    public void attack()
    {
        isAttacking = true;
        CanAttack = false;
        animation.SetBool("Punching", true);
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(punchSound);
        StartCoroutine(ResetAttackCooldown());
        StartCoroutine(ResetAttackBool());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
        animation.SetBool("Punching", false);
    }
    
    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false;
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
            if (Input.GetKeyDown(KeyCode.Space))
            { 
                playervelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
                animation.SetBool("Jumping", true);
                if (IsGrounded == false)
                {
                    animation.SetBool("Jumping", false);
                }
            }
        }
        else
        {
            animation.SetBool("Jumping", false);
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
        if (PlayerCrouch == false)
        {
            PlayerCrouch = true;
            animation.SetBool("Crouching", PlayerCrouch);
        }
        else
        {
            PlayerCrouch = false;
            animation.SetBool("Crouching", PlayerCrouch);
        }
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            PlayerCrouch = true;
            animation.SetBool("Running", PlayerRun);
            speed = 8;
        }
        else
        {
            PlayerRun = false;
            animation.SetBool("Running", PlayerRun);
            speed = 5;
        }
    }
}
