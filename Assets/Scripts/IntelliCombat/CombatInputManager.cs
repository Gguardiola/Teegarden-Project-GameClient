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
        menu.Pause.performed += ctx => pauseMenu.TogglePauseMenu(ui, false);

    }
    
    void Update()
    {
        apiClientHandler.CheckAPIError(ui, menu, false, false);
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
