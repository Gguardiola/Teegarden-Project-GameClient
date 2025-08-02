using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    [SerializeField]
    private GameObject crosshair;
    [SerializeField]
    private GameObject interactableCrosshair;
    public GameObject UIgameOverScreen;
    [HideInInspector]
    public Color defaultTextColor;
    public BasicAnimations basicAnimations;

    public List<GameObject> UIInventorySlots;
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
            interactableCrosshair.SetActive(true);
            crosshair.SetActive(false);
        }
        else
        {
            interactableCrosshair.SetActive(false);  
            crosshair.SetActive(true);
        }

    }
    
    public void ShowGameOverScreen()
    {
        if (UIgameOverScreen != null)
        {
            UIgameOverScreen.SetActive(true);
        }
    }

    public int UIAddItemToInventorySlot(Sprite itemSprite)
    {
        int slotIndex = 0;
        foreach (var item in UIInventorySlots)
        {
            Image itemImage = item.GetComponent<Image>();
            if (itemImage.sprite == null)
            {
                itemImage.sprite = itemSprite;
                item.SetActive(true);
                StartCoroutine(basicAnimations.AnimationPulse(item, item.transform.localScale, item.transform.localScale * 1.2f, 5f));
                return slotIndex;
            }
            slotIndex++;
        }

        return -1;
    }
    public void UIRemoveItemFromInventorySlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < UIInventorySlots.Count)
        {
            Image itemImage = UIInventorySlots[slotIndex].GetComponent<Image>();
            itemImage.sprite = null;
            UIInventorySlots[slotIndex].SetActive(false);
            Debug.Log("Removed item from slot index: " + slotIndex);;
        }
    }
}
