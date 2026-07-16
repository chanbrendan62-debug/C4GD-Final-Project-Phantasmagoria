using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : ENEMYBASE
{
    public bool damaging = false;
    SpriteRenderer spriteRenderer;

    public override void Start()
    {
        base.Start();
        health.OnDamaged += Damaged;
        spriteRenderer = anim.GetComponent<SpriteRenderer>();
    }

    public override void Idle()
    {
        base.Idle();
        anim.SetBool("walking", false);

        // OPTIONAL: Stop moving horizontally when idling
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public override void Chase()
    {
        if (damaging) { return; }

        float diff = PlayerInstance.Instance.transform.position.x - transform.position.x;
        int dir = (int)Mathf.Sign(diff);

        if (dir > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (dir < 0)
        {
            spriteRenderer.flipX = true;
        }

        // FIXED: Set the horizontal velocity directly, and let Unity's physics engine 
        // handle the vertical gravity naturally.
        rb.velocity = new Vector2(speed * dir, rb.velocity.y);

        anim.SetBool("walking", true);
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

        // Stop the knockback sliding before letting the enemy walk again
        //rb.velocity = new Vector2(0, rb.velocity.y);

        anim.SetBool("hit", false);
        damaging = false;
    }
}