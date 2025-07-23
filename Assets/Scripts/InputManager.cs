using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    public PlayerInput.DEBUGActions debug;
    private PlayerInput.MenuActions menu;
    private PlayerMovement movement;
    private PlayerLook look;
    public Gun gun;
    public PauseMenu pauseMenu;
    public APIClientHandler apiClientHandler;
    public PlayerHealth playerHealth;
    
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        menu = playerInput.Menu;
        
        onFoot.Jump.performed += ctx => movement.Jump();
        onFoot.Crouch.performed += ctx => movement.Crouch();
        onFoot.Sprint.performed += ctx => movement.Sprint();
        onFoot.Shoot.performed += ctx => gun.TryShoot();
        onFoot.Reload.performed += ctx => gun.TryReload();
        menu.Pause.performed += ctx => TogglePauseMenu();

    }

    void Update()
    {
        CheckFreezeConditions();
    }
    
    private void CheckFreezeConditions()
    {
        if (apiClientHandler != null && apiClientHandler.isError)
        {
            onFoot.Disable();
            menu.Disable();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else if (!apiClientHandler.isError && !pauseMenu.isPaused())
        {
            onFoot.Enable();
            menu.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;

        }

        if (playerHealth != null && playerHealth.isDead && !pauseMenu.isPaused())
        {
            onFoot.Disable();
            menu.Disable();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
    
    public void TogglePauseMenu()
    {
        pauseMenu.TogglePauseMenu();
        CheckIfPaused();

    }
    
    private void CheckIfPaused()
    {
        if (pauseMenu.isPaused())
        {
            onFoot.Disable();
        }
        else
        {
            onFoot.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void FixedUpdate()
    {
        movement.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    
    private void OnEnable()
    {
        onFoot.Enable();
        menu.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
        menu.Disable();
    }
}
