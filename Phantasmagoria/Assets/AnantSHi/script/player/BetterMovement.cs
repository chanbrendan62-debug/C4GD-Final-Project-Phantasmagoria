using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterMovement : MonoBehaviour
{
    [Header("movement")]
    public float speed;
    public float jumpPower;
    public float coyoteTime;
    
    int dJumpCount;
    public int maxDJump;

    float xVel;
    float yVel;

    [Header("physics")]
    public float radiusCheck = .1f;
    public LayerMask groundLayer;
    public Transform checker;

    [Header("Dash")]
    public float dashSpeed = 35f;
    public float dashDuration = 0.25f;
    float dashCooldown;
    
    public float maxDashCooldown = 1f;
    

    float originalGravity;
    bool isDashing = false;
    bool isInvincible = false;
    public bool canDash = true;


    [Header("Cosmetic")]
    public Animator anim;
    public SpriteRenderer sprite;

    bool jumped;
    float timeGrounded;

    


    
    Rigidbody2D rb;
    int lookDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        originalGravity = rb.gravityScale;

        dJumpCount = maxDJump;

    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) { return; }


        //moveinputshi
        float hor = Input.GetAxis("Horizontal");
        xVel = hor * speed;
        if (anim != null)
        {   
            anim.SetFloat("speed", Mathf.Abs(hor));
        }
        


        //jumpinputshi
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumped = true;
        }


        //flipshi
        if (hor < 0)
        {
            sprite.flipX = true;
            lookDir = -1;
        }
        else if (hor > 0)
        {
            sprite.flipX = false;
            lookDir = 1;
        }

        //dashshi
        dashCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown <= 0 && canDash)
        {
            StartCoroutine(Dash());
        }

    }
    private void FixedUpdate()
    {
        if(isDashing) { return; }


        //updatemovementshit
        yVel = rb.velocity.y;
        if (!isGrounded()) { timeGrounded += Time.deltaTime; } else { timeGrounded = 0; dJumpCount = maxDJump; }

        //animateshi
        if (anim != null)
        {
            anim.SetBool("grounded", isGrounded());
            anim.SetFloat("yVel", yVel);
        }


        //jumpshi
        if ((isGrounded() || timeGrounded < coyoteTime) && jumped)
        {
            yVel = jumpPower;
            timeGrounded = 20;
        } 
        else if(dJumpCount > 0 && jumped)
        {
            yVel = jumpPower;
            timeGrounded = 20;
            dJumpCount -= 1;
        }
            jumped = false;
        

        //moveeverythingshi
        rb.velocity = new Vector2(xVel, yVel);
    }


    bool isGrounded()
    {
        return Physics2D.OverlapCircle(checker.position, radiusCheck, groundLayer);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        isInvincible = true;

        dashCooldown = maxDashCooldown;
        rb.gravityScale = 0f;

        
        rb.velocity = new Vector2(lookDir * dashSpeed, 0f);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        rb.velocity = new Vector2(0f, rb.velocity.y);

        isDashing = false;
        isInvincible = false;
    }


}
