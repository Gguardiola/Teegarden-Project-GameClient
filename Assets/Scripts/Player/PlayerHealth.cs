using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    private float hp;
    private float maxHp = 100f;
    private float lerpTimer;
    private float chipSpeed = 2f;
    public TextMeshProUGUI healthText;
    public Image frontHealthBar;
    public Image backHealthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = maxHp;
        healthText.text = hp.ToString() + "/" + maxHp.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        hp = Mathf.Clamp(hp, 0, maxHp);
        UpdateHpUI();
    }

    public void UpdateHpUI()
    {
        Debug.Log("PLAYER HP: " + hp);
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
        Debug.Log("PLAYER TOOK DAMAGE: " + damage);
        hp -= damage;
        lerpTimer = 0f;

    }
    
    public void RestoreHealth(float amount)
    {
        Debug.Log("PLAYER RESTORED HEALTH: " + amount);
        hp += amount;
        lerpTimer = 0f;
    }
}
