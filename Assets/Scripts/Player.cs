using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private float direction = 0f;
    public float jumpForce;
    public Rigidbody2D rb;
    Vector3 originalPos;
    bool jumping = false;
    int jumpsLeft = 2;
    float slidingTimer;
    public float slidingTimerAmount = 0.75f;
    bool slidingCooldownStarter = false;
    float slidingCooldownTimer = 2f;
    public float slidingCooldownTimerAmount = 0.5f;
    bool canSlide = true;
    KeyCode slidingKey = KeyCode.LeftShift;
    bool sliding = false;
    public float slidingSpeed = 7;
    public float fallMultiplier = 2.5f;
    
    void Start() {
        originalPos = gameObject.GetComponent<Transform>().position;
        slidingTimer = slidingTimerAmount;
        slidingCooldownTimer = slidingCooldownTimerAmount;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            jumpsLeft = 2;

        if (col.gameObject.tag == "Enemy")
        {
            transform.position = originalPos;
            rb.velocity = Vector2.zero;
        }
    }

    void Update() {
        //Makes it so you fall faster after you jump or things like that
        /*if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;*/
        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
            jumping = true;

        if (Input.GetKeyDown(slidingKey) && canSlide)
        {
            //Change Sprite to sliding here
            //Change hitbox here
            direction = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(direction * slidingSpeed, rb.velocity.y);
            sliding = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            if (slidingTimer > 0f)
                slidingTimer -= Time.deltaTime;
            else
            {
                slidingTimer = slidingTimerAmount;
                slidingCooldownStarter = true;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                canSlide = false;
                sliding = false;
            }
        }

        if (slidingCooldownStarter)
        {
            if (slidingCooldownTimer > 0f)
                slidingCooldownTimer -= Time.deltaTime;
            else
            {
                slidingCooldownTimer = slidingCooldownTimerAmount;
                canSlide = true;
                slidingCooldownStarter = false;
            }
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else if (Input.GetAxisRaw("Horizontal") < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        if (Input.GetKeyDown(KeyCode.R)) {
            transform.position = originalPos;
            rb.velocity = Vector2.zero;
        }
    }

    void FixedUpdate() {
        //tell alon that there's no need to put these commands in fixedupdate. we should move them to normal update
        if (jumping == true) {
            if (jumpsLeft == 1)
                rb.velocity = new Vector2(rb.velocity.x, jumpForce / 1.25f);
            else if (jumpsLeft == 2)
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsLeft -= 1;
            jumping = false;
        }
        if (sliding == false)
        {
            direction = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }
    }
}
