using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    public PlayerInput.DEBUGActions debug;
    private PlayerMovement movement;
    private PlayerLook look;
    public Gun gun;
    
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        
        onFoot.Jump.performed += ctx => movement.Jump();
        onFoot.Crouch.performed += ctx => movement.Crouch();
        onFoot.Sprint.performed += ctx => movement.Sprint();
        onFoot.Shoot.performed += ctx => gun.TryShoot();
        onFoot.Reload.performed += ctx => gun.TryReload();

    }

    private void FixedUpdate()
    {
        //PlayerMovement movement
        movement.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
