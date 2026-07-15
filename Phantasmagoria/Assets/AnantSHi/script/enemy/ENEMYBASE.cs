using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMYBASE : MonoBehaviour
{
    public float damage;
    public float knockback;
    public float speed;
    public float attakcRange;
    public float chaseRange;
    public Animator anim;
    protected bool isAttacking;
    protected Rigidbody2D rb;
    protected BetterHealth health;

    public virtual void Start()
    {
        hitMask hit = GetComponent<hitMask>();
        if (hit != null)
        {
            hit.damage = damage;
            hit.knockback = knockback;
        }
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<BetterHealth>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerInstance.Instance == null) { return; }

        float dis = Vector2.Distance(transform.position, PlayerInstance.Instance.transform.position);
        if (dis <= attakcRange)
        {
            Attack();
        }
        else if (dis <= chaseRange)
        {
            Chase();
        }
        else
        {
            Idle();
        }
    }

    public virtual void Idle()
    {

    }

    public virtual void Chase()
    {
        if (isAttacking) { return; }
    }

    public virtual void Attack()
    {

    }
}
