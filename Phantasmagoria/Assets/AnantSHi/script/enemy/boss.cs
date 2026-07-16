using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class boss : ENEMYBASE
{
    public float biggerRange;

    // projectile instantiate
    public GameObject projectile;
    public float projSpeed;
    public float projDamage;
    public float projKnockback;

    // cooldowns
    public float projCooldown;
    float currentCooldown;

    public bool damaging = false;

    public override void Start()
    {
        base.Start();
        health.OnDamaged += Damaged;
    }

    public override void Idle()
    {
        if (damaging) { return; }

        base.Idle();
        currentCooldown = projCooldown;
        anim.SetBool("chasing", false);

        Transform sprite = anim.transform;
        sprite.transform.rotation = Quaternion.Euler(0, 0, 0);

        // OPTIONAL: Stop horizontal movement when idling, while letting gravity do its thing
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public override void Chase()
    {
        if (damaging) { return; }

        chaseRange = biggerRange;
        anim.SetBool("chasing", true);
        Transform sprite = anim.transform;

        float diff = PlayerInstance.Instance.transform.position.x - transform.position.x;
        int dir = (int)Mathf.Sign(diff);

        sprite.transform.rotation = Quaternion.Euler(0, 0, 12.76f * -dir);

        // FIXED: Set the velocity directly, preserving gravity (rb.velocity.y)
        rb.velocity = new Vector2(speed * dir, rb.velocity.y);

        // projectile Timer
        if (currentCooldown < 0)
        {
            projtile(dir);
            currentCooldown = projCooldown;
        }
        currentCooldown -= Time.deltaTime;
    }

    void projtile(int dir)
    {
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
        projectile ctile = proj.GetComponent<projectile>();
        ctile.dmg = projDamage;
        ctile.knockback = projKnockback;
        ctile.speed = projSpeed;

        ctile.dir = new Vector2(dir, 0);
    }

    void Damaged(float damage)
    {
        damaging = true;
        anim.SetBool("hit", true);
        StartCoroutine(Knockback());
    }

    IEnumerator Knockback()
    {
        yield return new WaitForSeconds(health.iFrames);

        

        anim.SetBool("hit", false);
        damaging = false;
    }




}