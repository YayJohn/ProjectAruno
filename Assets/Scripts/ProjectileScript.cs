using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float moveSpeed = 7f;
    public Rigidbody2D rb;
    public GameObject player;
    Vector2 moveDirection;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            moveDirection = (player.transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        }
    }
}
