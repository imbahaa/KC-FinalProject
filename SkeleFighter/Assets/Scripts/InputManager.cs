using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerinput;
    public PlayerInput.OnFootActions onfoot;
    private PlayerMovements motor;
    private PlayerLook look;
    // Start is called before the first frame update
    void Awake()
    {
        playerinput = new PlayerInput();
        onfoot = playerinput.OnFoot;
        motor = GetComponent<PlayerMovements>();
        look = GetComponent<PlayerLook>();
        onfoot.Jump.performed += ctx => motor.Jump();
        onfoot.Crouch.performed += ctx => motor.Crouch();
        onfoot.Sprint.performed += ctx => motor.Sprint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcesssMove(onfoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.ProcessLook(onfoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onfoot.Enable();
    }
    private void OnDisable()
    {
        onfoot.Disable();
    }
}
