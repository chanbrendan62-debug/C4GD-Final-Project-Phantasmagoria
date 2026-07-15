using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private DeathMenu deathMenu;
    public float MaxHP = 100;
    public float currentHP = 100;
    public float MaxFear = 100;
    public float currentFear = 100;
    void Start()
    {
        currentHP = MaxHP;
        currentFear = MaxFear;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amt)
    {   
        currentHP -= amt;

        if (currentHP <= 0)
        {
            if (deathMenu != null)
            {
                deathMenu.TriggerDeath();
            }
        }
    }

    public void GainHealth(float amt)
    {   
        if(currentHP >= MaxHP)
        {
            currentHP += 0;
        }
        else
        {
            currentHP += amt;
        }
    }

    public void GainTotalHealth(float amt)
    {   
        MaxHP += 5;
    }

    public void TakeFear(float amt)
    {   
        currentFear -= amt;

        if(currentFear <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GainBravery(float amt)
    {   
        if(currentFear >= MaxFear)
        {
            currentFear += 0;
        }
        else
        {
            currentFear += amt;
        }
    }

    public void GainTotalBravery(float amt)
    {   
        MaxFear += 5;
    }
}
