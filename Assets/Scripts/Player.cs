using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private float direction = 0f;
    public float jumpForce;
    public Rigidbody2D rb;
    Transform enemy;
    Vector3 originalPos;
    bool jumping = false;
    int jumpsLeft = 2;
    
    void Start() {
      //  originalPos.position = transform.position;
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        originalPos = gameObject.GetComponent<Transform>().position;
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
        if (Input.GetKeyDown(KeyCode.R)) {
            transform.position = originalPos;
            rb.velocity = Vector2.zero;
        }

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            jumping = true;
            jumpsLeft -= 1;
        }
    }

    void FixedUpdate() {
        if (jumping == true) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumping = false;
        }
        direction = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
}
