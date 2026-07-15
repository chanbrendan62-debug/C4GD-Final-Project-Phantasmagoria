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
        float newY = startPos.y + Mathf.Cos(Time.time * bobSpeed + offset) * bobHeight;
        rb.MovePosition(new Vector2(transform.position.x, newY));
    }

    void Damaged(float damge)
    {
        damaging = true;
        StartCoroutine(Knockback());

    }

    IEnumerator Knockback()
    {
        yield return new WaitForSeconds(health.iFrames);
        startPos = transform.position;
        damaging = false;
    }
}
