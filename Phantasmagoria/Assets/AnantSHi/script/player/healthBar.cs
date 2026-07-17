using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public BetterHealth health; // drag the Player in via the Inspector
    public Image fillHealth;

    void Start()
    {
        health.OnDamaged += onDamage;
        health.OnHeal += onHeal;
        fillHealth.fillAmount = 1;
    }

    void onDamage(float dmg)
    {
        fillHealth.fillAmount = health.currentHealth / health.maxHealth;
    }

    void onHeal(float heal)
    {
        fillHealth.fillAmount = health.currentHealth / health.maxHealth;
    }
}