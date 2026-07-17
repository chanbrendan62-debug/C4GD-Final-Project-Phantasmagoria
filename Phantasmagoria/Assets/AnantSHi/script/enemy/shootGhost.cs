using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class shootGhost : ENEMYBASE
{
    private Vector2 startPos;
    public float bobHeight = 0.5f;
    public float bobSpeed = 2f;

    public GameObject projectile;
    public float projSpeed;
    public float projDamage;
    public float projKnockback;

    //cooldowns
    public float projCooldown;
    float currentCooldown;


    float offset;
    bool damaging;



    public override void Start()
    {
        base.Start();
        startPos = transform.position;
        offset = Random.Range(0f, Mathf.PI * 2f);
        health.OnDamaged += Damaged;
        currentCooldown = projCooldown;
    }

    public override void Idle()
    {
        if (damaging) { return; }
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed + offset) * bobHeight;
        float verticalVelocity = (newY - transform.position.y) * bobSpeed;

        
        rb.velocity = new Vector2(0, verticalVelocity);
    }

    public override void Chase()
    {
        if (damaging) { return; }
        Vector2 playerPos = PlayerInstance.Instance.transform.position;
        Vector2 targetpos = new Vector2(playerPos.x, playerPos.y + 3);
        Vector2 direction = (targetpos - (Vector2)transform.position).normalized;

        // Move by setting velocity so physics engine stays perfectly synced
        rb.velocity = direction * speed;

    }
    public override void Attack()
    {
        if (damaging) { return; }

        if (currentCooldown < 0)
        {
            projtile();
            currentCooldown = projCooldown;
        }
        currentCooldown -= Time.deltaTime;

    }

    void Damaged(float damge)
    {
        damaging = true;
        anim.SetBool("hit", true);
        StartCoroutine(Knockback());

    }

    IEnumerator Knockback()
    {
        yield return new WaitForSeconds(health.iFrames);
        startPos = transform.position;
        anim.SetBool("hit", false);
        damaging = false;
    }


    void projtile()
    {
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
        projectile ctile = proj.GetComponent<projectile>();
        ctile.dmg = projDamage;
        ctile.knockback = projKnockback;
        ctile.speed = projSpeed;


        Vector2 shootDir = ((Vector2)PlayerInstance.Instance.transform.position - (Vector2)transform.position).normalized;
        ctile.dir = shootDir;

    }


}
