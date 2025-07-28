using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [Header("Health Bar")]
    public float hp;
    public float maxHp;
    private float chipSpeed = 2f;
    public TextMeshProUGUI healthText;
    public Image frontHealthBar;
    public Image backHealthBar;
    private float lerpTimer;
    public bool isDead
    {
        get { return hp <= 0; }
    }

    [Header("Player overlay")] 
    public PlayerUI playerUI;

    public Image overlay;
    private float overlayDuration = 2f;
    private float overlayFadeSpeed = 5f;
    private float overlayFadeDuration;
    private float damageAmountToAlpha;
    

    void Start()
    {
        maxHp = PlayerConfig.Instance.maxHealth;
        hp = maxHp;
        healthText.text = hp.ToString() + "/" + maxHp.ToString();
    }

    void Update()
    {
        UpdateHpBar();
        UpdateDamageOverlay();
    }

    public void UpdateDamageOverlay()
    {
        damageAmountToAlpha = Mathf.Clamp(damageAmountToAlpha, 0, 1);
        if(overlay.color.a > 0)
        {
            if (hp < PlayerConfig.Instance.criticalHealth)
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

    public void UpdateHpBar()
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
        if (hp < PlayerConfig.Instance.criticalHealth)
        {
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, damageAmountToAlpha);
        }
        if (hp <= 0)
        {
            playerUI.ShowGameOverScreen();
        }
        
    }
    
    public void RestoreHealth(float amount)
    {
        hp += amount;
        lerpTimer = 0f;
    }
}
