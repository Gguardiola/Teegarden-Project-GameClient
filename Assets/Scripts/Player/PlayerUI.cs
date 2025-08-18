using System;
using System.Collections;
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
    private Vector3 crosshairAttackTransformOriginalScale;
    private Coroutine crosshairAttackCoroutine;
    [SerializeField]    
    private GameObject interactableCrosshair;
    public GameObject UIgameOverScreen;
    public GameObject UINoAmmoLabel;
    public GameObject UIHitmarker;
    public Image UIReloadingLabelFront;
    public Image UIReloadingLabelBack;
    public TextMeshProUGUI UILevelInfoNameText;
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
        crosshairAttackTransformOriginalScale = crosshair.transform.localScale;
        UIUpdateLevelName();

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

    public void UISetNoAmmoLabel()
    {
            UINoAmmoLabel.SetActive(true);
            StartCoroutine(basicAnimations.AnimationPulse(UINoAmmoLabel, UINoAmmoLabel.transform.localScale, UINoAmmoLabel.transform.localScale * 1.2f, 20f));
    }
    
    public void UIUnSetNoAmmoLabel()
    {
            UINoAmmoLabel.SetActive(false);
    }

    public IEnumerator ShowHitmarker()
    {
        if (UIHitmarker != null)
        {
            UIHitmarker.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            UIHitmarker.SetActive(false);
        }
    }

    public void UICrosshairBoop()
    {
        if (crosshairAttackCoroutine != null)
        {
            StopCoroutine(crosshairAttackCoroutine);
        }
        crosshairAttackCoroutine= StartCoroutine(basicAnimations.AnimationPulseInterruptable(crosshair, crosshairAttackTransformOriginalScale, crosshair.transform.localScale * 1.4f, 30f));
    }

    public void UIShowReloadingAnimation()
    {
        UIReloadingLabelFront.gameObject.SetActive(true);
        UIReloadingLabelBack.gameObject.SetActive(true);
        UIReloadingLabelFront.fillAmount = 0f;
        UIReloadingLabelBack.fillAmount = 1f;
        StartCoroutine(ReloadingAnimation());
    }

    private IEnumerator ReloadingAnimation()
    {
        float time = 0f;
        float duration = 1f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float fillAmount = Mathf.Clamp01(time / duration);
            UIReloadingLabelFront.fillAmount = fillAmount;
            yield return null;
        }
        UIReloadingLabelFront.gameObject.SetActive(false);
        UIReloadingLabelBack.gameObject.SetActive(false);
        UIReloadingLabelFront.fillAmount = 0f;
    }

    public void UIUpdateLevelName()
    {
        if (UILevelInfoNameText != null)
        {
            string levelName = LevelContext.Instance.currentLevelName;
            UILevelInfoNameText.text = levelName;
        }
    }
}
