using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D rb;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(7,7);
        Physics2D.IgnoreLayerCollision(7, 6);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        Destroy(this.gameObject);
    }
}
