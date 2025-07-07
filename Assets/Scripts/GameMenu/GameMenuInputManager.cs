using System.Collections;
using UnityEngine;

public class GameMenuInputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.UIActions ui;
    public PlayerInput.DEBUGActions debug;
    public Camera sceneCamera;
    private GameMenuButton lastClickedButton;
    void Awake()
    {
        playerInput = new PlayerInput();
        ui = playerInput.UI;
        ui.Click.performed += ctx => Click();

    }
    
    void Start()
    {
        StartCoroutine(BlockInputStartup());
    }

    private IEnumerator BlockInputStartup()
    {
        ui.Disable();
        yield return null;
        ui.Enable();
    }

    private void Click()
    {
        if (InputBlocker.IsBlocked) return;
        Vector3 mousePos = Input.mousePosition;
        if (sceneCamera != null)
        {
            Ray ray = sceneCamera.ScreenPointToRay(mousePos);
            bool raycastHit = Physics.Raycast(ray, out RaycastHit hit);
            if (raycastHit && hit.collider != null && hit.collider.GetComponent<GameMenuButton>() != null)
            {
                GameMenuButton clickedButton = hit.collider.GetComponent<GameMenuButton>();
                if (clickedButton != null)
                {
                    if (lastClickedButton != null)
                    {
                        lastClickedButton.CleanUp();
                    }
                    lastClickedButton = clickedButton;
                    clickedButton.OnClick();
                    InputBlocker.BlockForOneFrame();
                }
            }
        }
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