using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{

    [Header("Movement")]
    public float speed;
    public float jumpPower;
    public float coyoteTime;
    public KeyCode jumpKey;

    float xVel;
    float yVel;

    [Header("Physics")]
    public float radiusCheck = .1f;
    public LayerMask groundLayer;
    public Transform checker;

    bool jumped;
    float timeGrounded;

    [Header("Dash")]
    public float dashSpeed = 35f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 1f;
    
    public bool isDashing = false;

    public bool isInvincible = false;
    public float dashEndTime;
    public float nextDashTime;

    public float originalGravity;
    Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {   
        if (isDashing)
        {
           return;
        }
        //moveshi
        float hor = Input.GetAxis("Horizontal");
        xVel = hor * speed;

        //jumpshi

        if (Input.GetKeyDown(jumpKey))
        {
            jumped = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextDashTime)
        {
            StartCoroutine(Dash());
        }

        if(hor < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(hor > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        
    }
    private void FixedUpdate()
    {
        //updatemovementshit
        if (isDashing)
        {
            return;
        }
        yVel = rb.velocity.y;
         if (!isGrounded()) { timeGrounded += Time.deltaTime; } else { timeGrounded = 0; }
        if((isGrounded() || timeGrounded < coyoteTime) && jumped)
        {
            yVel = jumpPower;
            timeGrounded = 20;
        }
        jumped = false;
        rb.velocity = new Vector2(xVel, yVel);
    }



    bool isGrounded()
    {
        return Physics2D.OverlapCircle(checker.position, radiusCheck, groundLayer);
    }

    IEnumerator Dash() 
    {

        isInvincible = true;

        isDashing = true;
        isInvincible = true; 

        nextDashTime = Time.time + dashDuration + dashCooldown;
        rb.gravityScale = 0f;

        float dashDirection = transform.localScale.x;
        rb.velocity = new Vector2(dashDirection * dashSpeed, 0f);

        yield return new WaitForSeconds(0.5f);

        rb.gravityScale = originalGravity;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        
        isDashing = false;
        isInvincible = false;

    } 



}