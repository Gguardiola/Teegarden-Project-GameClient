using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class CombatInputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.UIActions ui;
    public PlayerInput.DEBUGActions debug;
    private CombatManager combatManager;
    void Awake()
    {
        playerInput = new PlayerInput();
        combatManager = GetComponent<CombatManager>();
        ui = playerInput.UI;
        
        ui.Click.performed += ctx => combatManager.Click();

    }
    
    private void OnEnable()
    {
        ui.Enable();
    }
    private void OnDisable()
    {
        ui.Disable();
    }
}
