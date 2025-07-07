using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class CombatInputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.UIActions ui;
    public PlayerInput.MenuActions menu;
    public PlayerInput.DEBUGActions debug;
    private CombatManager combatManager;
    private PauseMenu pauseMenu;
    public APIClientHandler apiClientHandler;
    void Awake()
    {
        playerInput = new PlayerInput();
        pauseMenu = GetComponent<PauseMenu>();
        combatManager = GetComponent<CombatManager>();
        ui = playerInput.UI;
        menu = playerInput.Menu;
        
        ui.Click.performed += ctx => combatManager.Click();
        menu.Pause.performed += ctx => TogglePauseMenu();

    }

    private void TogglePauseMenu()
    {
        pauseMenu.TogglePauseMenu();
        CheckIfPaused();
    }
    
    void Update()
    {
        CheckIfAPIError();
    }
    
    private void CheckIfAPIError()
    {
        if (apiClientHandler != null && apiClientHandler.isError)
        {
            ui.Disable();
            menu.Disable();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else if (!apiClientHandler.isError && !pauseMenu.IsPaused())
        {
            ui.Enable();
            menu.Enable();
            Time.timeScale = 1f;

        }
    }
    
    private void CheckIfPaused()
    {
        if (pauseMenu.IsPaused())
        {
            ui.Disable();
        }
        else
        {
            ui.Enable();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    
    private void OnEnable()
    {
        ui.Enable();
        menu.Enable();
    }
    private void OnDisable()
    {
        ui.Disable();
        menu.Disable();
    }
}
