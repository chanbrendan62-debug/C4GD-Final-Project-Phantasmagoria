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

    public override void Idle()
    {
        if (damaging) { return; }
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed + offset) * bobHeight;
        rb.MovePosition(new Vector2(transform.position.x, newY));
    }

    public override void Chase()
    {
        Vector2 playerPos = PlayerInstance.Instance.transform.position;
        Vector2 direction = (playerPos - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

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
        startPos = transform.position;
        anim.SetBool("damaging", false);
        damaging = false;
    }
}
