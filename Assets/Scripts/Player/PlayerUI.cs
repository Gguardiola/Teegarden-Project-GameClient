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
    [HideInInspector]
    public Color defaultTextColor;
    public Color PromptTextColor
    {
        get => promptText.color;
        set => promptText.color = value;
    }    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        defaultTextColor = promptText.color;
        PromptTextColor = defaultTextColor;
    }

    // Update is called once per frame
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
}
