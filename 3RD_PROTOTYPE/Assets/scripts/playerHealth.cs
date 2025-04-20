using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerHealth : MonoBehaviour
{
    public float maxHealth = 5f;
    public float currentHealth;
    public Image healthBar;
    public TextMeshProUGUI healthText;

  

    public void Start()
    {
        currentHealth = maxHealth;
        updateHealthBar();
    }

    public void updateHealth(float amount)
    {
        currentHealth += amount;
        updateHealthBar();

    }

    public void updateHealthBar()
    {
        float targetFillAmount = currentHealth / maxHealth;
        healthBar.fillAmount = targetFillAmount;
    }

    [ContextMenu("PlayerHit")]
    public void PlayerHit()
    {
        currentHealth = currentHealth - 1f;
        updateHealthBar();
        healthText.text = currentHealth.ToString();

    }

    [ContextMenu("PlayerHeal")]
    public void PlayerHeal()
    {
        currentHealth = currentHealth + 1f;
        updateHealthBar();
        healthText.text = currentHealth.ToString();

    }
}
