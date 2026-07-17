using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] private DeathMenu deathMenu;

    public float damage;

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

    public int totalJumps = 1;
    public int currentJumps;

    [Header("Dash")]
    public float dashSpeed = 35f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 1f;

    public bool isDashing = false;

    public bool isInvincible = false;
    public float dashEndTime;
    public float nextDashTime;

    public int totalDashCount;
    public int currentDashCount;

    public float originalGravity;

    public Animator anim;
    float gameOverHeight = -10f;
    Rigidbody2D rb;

    BetterHealth health;

    void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        currentJumps = totalJumps;

        health = GetComponent<BetterHealth>();
        health.OnDeath += HandleDeath;
    }

    void HandleDeath()
    {
        if (deathMenu != null)
        {
            deathMenu.TriggerDeath();
        }
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        float hor = Input.GetAxis("Horizontal");
        xVel = hor * speed;

        if (anim != null)
        {
            anim.SetFloat("speed", Mathf.Abs(hor));
        }

        if (Input.GetKeyDown(jumpKey) && currentJumps > 0)
        {
            jumped = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextDashTime)
        {
            StartCoroutine(Dash());
        }

        if (hor < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (hor > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (transform.position.y < gameOverHeight)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        yVel = rb.velocity.y;
        if (!isGrounded())
        {
            timeGrounded += Time.fixedDeltaTime;
        }
        else
        {
            timeGrounded = 0;
            currentJumps = totalJumps;
        }

        if (anim != null)
        {
            anim.SetBool("grounded", isGrounded());
            anim.SetFloat("yVel", yVel);
        }

        if (jumped)
        {
            if (isGrounded())
            {
                yVel = jumpPower;
                timeGrounded = coyoteTime + 1f;
                currentJumps--;
            }
            else if (currentJumps > 0 && totalJumps > 1)
            {
                yVel = jumpPower;
                currentJumps--;
            }
            jumped = false;
        }

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

        nextDashTime = Time.time + dashDuration + dashCooldown;
        rb.gravityScale = 0f;

        float dashDirection = transform.localScale.x;
        rb.velocity = new Vector2(dashDirection * dashSpeed, 0f);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        rb.velocity = new Vector2(0f, rb.velocity.y);

        isDashing = false;
        isInvincible = false;
    }

    public void addJumps()
    {
        totalJumps += 1;
    }

    public void increaseDamage(float amt)
    {
        damage += amt;
    }
}