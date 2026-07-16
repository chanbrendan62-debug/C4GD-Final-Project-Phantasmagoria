using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;
     Rigidbody2D rb;
    public Vector2 dir;
    public float dmg;
    public float knockback;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitMask hit = GetComponent<hitMask>();
        hit.damage = dmg;
        hit.knockback = knockback;

        Invoke("die", 5);



    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, speed);


        rb.velocity = dir * speed;
    }

    void die()
    {
        Destroy(gameObject);
    }
}
