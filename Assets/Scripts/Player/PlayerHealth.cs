using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    private float hp;
    private float maxHp;
    [Header("Health Bar")]
    private float chipSpeed = 2f;
    public TextMeshProUGUI healthText;
    public Image frontHealthBar;
    public Image backHealthBar;
    private float lerpTimer;

    [Header("Player overlay")] 
    public Image overlay;
    private float overlayDuration;
    private float overlayFadeSpeed;
    private float overlayFadeDuration;
    private float damageAmountToAlpha;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHp = PlayerConfig.Instance.maxHealth;
        hp = maxHp;
        healthText.text = hp.ToString() + "/" + maxHp.ToString();
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);

        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHpUI();
        UpdateDamageOverlay();
    }

    public void UpdateDamageOverlay()
    {
        damageAmountToAlpha = Mathf.Clamp(damageAmountToAlpha, 0, 1);
        if(overlay.color.a > 0)
        {
            if (hp < 30)
            {
                return;
            }
            overlayFadeDuration += Time.deltaTime;
            if (overlayFadeDuration > overlayDuration)
            {
                float damageAmountToAlphaFade = overlay.color.a;
                damageAmountToAlphaFade -= Time.deltaTime * overlayFadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, damageAmountToAlphaFade);
            }
        }        
    }

    public void UpdateHpUI()
    {
        hp = Mathf.Clamp(hp, 0, maxHp);
        
        float fillFrontBar = frontHealthBar.fillAmount;
        float fillBackBar = backHealthBar.fillAmount;
        float hFraction = hp / maxHp;
        healthText.text = hp.ToString() + "/" + maxHp.ToString();
        if (fillBackBar > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBackBar, hFraction, percentComplete);
        }
        else if (fillFrontBar < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFrontBar, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        lerpTimer = 0f;
        overlayFadeDuration = 0f;
        damageAmountToAlpha = ((maxHp / 100f) - (hp / maxHp));
        Debug.Log("fade: " + damageAmountToAlpha);
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, damageAmountToAlpha);


    }
    
    public void RestoreHealth(float amount)
    {
        Debug.Log("PLAYER RESTORED HEALTH: " + amount);
        hp += amount;
        lerpTimer = 0f;
    }
}
