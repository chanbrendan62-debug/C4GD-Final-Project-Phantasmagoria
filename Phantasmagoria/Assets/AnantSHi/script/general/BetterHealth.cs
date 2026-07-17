using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float iFrames;

    float maxiFrames;
    bool isDead = false;

    //subscription voids
    public event System.Action OnDeath;
    public event System.Action<float> OnDamaged;
    public event System.Action<float> OnHeal;
   
    void Start()
    {
        maxiFrames = iFrames;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        iFrames -= Time.deltaTime;
    }

    public void DMG(float damage)
    {
        if (iFrames > 0) { return; }
        if (isDead) { return; }
        iFrames = maxiFrames;
        currentHealth -= damage;
        OnDamaged?.Invoke(damage);

        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    public void Heal(float heal)
    {
        if (isDead) { return; }
        currentHealth += heal;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        OnHeal?.Invoke(heal);
    }

    public void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
