using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyer : ENEMYBASE
{
    private Vector2 startPos;
    public float bobHeight = 0.5f;
    public float bobSpeed = 2f;
    float offset;
    bool damaging;

    public override void Start()
    {
        base.Start();
        startPos = transform.position;
        offset = Random.Range(0f, Mathf.PI * 2f);
        health.OnDamaged += Damaged;
    }

    // FIXED: Uses velocity instead of MovePosition
    public override void Idle()
    {
        if (damaging) { return; }

        // Calculate the target Y position for bobbing
        float targetY = startPos.y + Mathf.Sin(Time.time * bobSpeed + offset) * bobHeight;

        // Calculate the vertical velocity needed to smoothly bob to that position
        float verticalVelocity = (targetY - transform.position.y) * bobSpeed;

        // Apply horizontal (0) and vertical velocity
        rb.velocity = new Vector2(0, verticalVelocity);
    }

    // FIXED: Uses velocity instead of MovePosition
    public override void Chase()
    {
        if (damaging) { return; }

        Vector2 playerPos = PlayerInstance.Instance.transform.position;
        Vector2 direction = (playerPos - (Vector2)transform.position).normalized;

        // Apply velocity directly in the direction of the player
        rb.velocity = direction * speed;
    }

    void Damaged(float damge)
    {
        damaging = true;
        anim.SetBool("damaging", true);
        StartCoroutine(Knockback());
    }

    IEnumerator Knockback()
    {
        yield return new WaitForSeconds(health.iFrames);

        // Cleanly stop the knockback momentum before resuming movement
        rb.velocity = Vector2.zero;

        // Reset startPos to the flyer's new position after being knocked back
        startPos = transform.position;

        anim.SetBool("damaging", false);
        damaging = false;
    }
}