using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    [SerializeField]
    private Sprite innerCrosshair;
    [SerializeField]
    private Sprite outerCrosshair;
    [SerializeField]
    private GameObject crosshair;
    public GameObject UIgameOverScreen;
    [HideInInspector]
    public Color defaultTextColor;
    public Color promptTextColor
    {
        get => promptText.color;
        set => promptText.color = value;
    }    
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        defaultTextColor = promptText.color;
        promptTextColor = defaultTextColor;
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
        if (promptMessage != String.Empty)
        {
            Image crosshairImage = crosshair.GetComponent<Image>();
            crosshairImage.sprite = innerCrosshair;           
        }
        else
        {
            Image crosshairImage = crosshair.GetComponent<Image>();
            crosshairImage.sprite = outerCrosshair;
        }

    }
    
    public void ShowGameOverScreen()
    {
        if (UIgameOverScreen != null)
        {
            UIgameOverScreen.SetActive(true);
        }
    }
}
