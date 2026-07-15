using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitMask : MonoBehaviour
{
    public float damage;
    public float knockback;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitEm(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitEm(collision.gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        HitEm(other);
    }
    void HitEm(GameObject thing)
    {
        BetterHealth colHealth = thing.GetComponent<BetterHealth>();
        Rigidbody2D rb = thing.GetComponent<Rigidbody2D>();

        if (colHealth != null && rb != null)
        {
            Vector2 knockbackDir = ((Vector2)thing.transform.position - (Vector2)transform.position).normalized;

            rb.AddForce(knockbackDir * knockback, ForceMode2D.Impulse);
            colHealth.DMG(damage);



        }
    }
}
