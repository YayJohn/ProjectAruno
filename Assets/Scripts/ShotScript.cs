using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public float moveSpeed = 7f;
    public Rigidbody2D rb;
    public GameObject player;
    Vector2 moveDirection;

    public void Shooting() {
        moveDirection = (player.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }
}
