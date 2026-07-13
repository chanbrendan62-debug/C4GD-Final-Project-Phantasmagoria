using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrendanPlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    float horizontalInput;
    public float moveSpeed = 10f;
    public float jumpSpeed = 5f;
    public Transform GroundCheckPoint;
    public LayerMask GroundLayer;
    public float groundCheckRadius = 0.5f;

    public float gameOverHeight = -4;

    public float dashSpeed = 35f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 1f;
    
    private bool isDashing = false;
    private float dashEndTime;
    private float nextDashTime;
    private float originalGravity;

    public float totalJump = 2;

    public float currentJump;



    float nextVelocityX;
    float nextVelocityY;
    Animator anim;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalGravity = rb2d.gravityScale;
    }

    void Update()
    {

        if (isDashing)
        {
            if (Time.time >= dashEndTime)
            {
                rb2d.gravityScale = originalGravity;
                rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
                isDashing = false;
            }
            else
            {
                return;
            }
        }

        horizontalInput = Input.GetAxis("Horizontal");
        float nextVelocityX = horizontalInput * moveSpeed;
        float nextVelocityY = rb2d.velocity.y;
        
        bool isGrounded = CheckGrounded();

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextDashTime)
        {
            Dash();
            return;
        }

        if(isGrounded && Input.GetKeyDown(KeyCode.Space) && currentJump > 0f)
        {   
            nextVelocityY = jumpSpeed;
            currentJump --;
        }

        rb2d.velocity = new Vector2(nextVelocityX, nextVelocityY);

        if(horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if(transform.position.y < gameOverHeight){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        anim.SetFloat("XSpeed", Mathf.Abs(nextVelocityX));
        anim.SetFloat("YSpeed", nextVelocityY);
        anim.SetBool("Grounded", isGrounded);
    }

    void Dash()
    {
        isDashing = true;
        
        dashEndTime = Time.time + dashDuration;
        nextDashTime = Time.time + dashDuration + dashCooldown;
        
        rb2d.gravityScale = 0f;

        float dashDirection = transform.localScale.x;
        rb2d.velocity = new Vector2(dashDirection * dashSpeed, 0f);
    }

    bool CheckGrounded()
    {   
        currentJump = totalJump;
        return Physics2D.OverlapCircle(GroundCheckPoint.position, groundCheckRadius, GroundLayer);
    }
}