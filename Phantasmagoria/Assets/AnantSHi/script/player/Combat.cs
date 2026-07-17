using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject hurtbox;
    GameObject hurt;    
    public float damage;
    public float knockback;
    public float delay;

    bool canHit = true;
    BetterHealth health;

    public Animator anim;
    public DeathMenu death;

    void Start()
    {
        hurtbox.SetActive(false);
        hurt = hurtbox.transform.Find("box").gameObject;
        hitMask hithurt = hurt.GetComponent<hitMask>();
        hithurt.damage = damage;
        hithurt.knockback = knockback;
        health = GetComponent<BetterHealth>();

        health.OnDamaged += Damaged;
        health.OnDeath += dead;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canHit)
        {
            StartCoroutine(attack());
        }


    }

    void Damaged(float dmg)
    {
        StartCoroutine(stopMove(GetComponent<BetterMovement>()));
        
    }

    IEnumerator stopMove(BetterMovement chara)
    {
        chara.enabled = false;
        anim.SetBool("hit", true);
        yield return new WaitForSeconds(.8f);
        anim.SetBool("hit", false);
        chara.enabled = true;
    }

    IEnumerator attack()
    {
        canHit = false;

        hurtbox.SetActive(true);
        particle.Play();
        hurt.GetComponent<Animator>().SetTrigger("follow");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = (mousePos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        hurtbox.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        yield return new WaitForSeconds(delay);
        hurtbox.SetActive(false);
        canHit = true;
    }

    void dead()
    {
        death.TriggerDeath();
    }
}
