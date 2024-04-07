using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public GameObject impactEffect;
    private Rigidbody2D rb;
    SoundManager soundManager;
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
        Physics2D.IgnoreLayerCollision(7,7);
        Physics2D.IgnoreLayerCollision(7, 6);
        soundManager.PlaySFX(soundManager.bulletShoot);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        soundManager.PlaySFX(soundManager.bulletHit);
        Debug.Log(col.name);
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
