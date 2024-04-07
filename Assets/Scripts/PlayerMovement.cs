using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float anglePerSecond;
    [SerializeField] private float swimForce;
    private Animator anim;
    private Rigidbody2D rb;
    public bool playerHitRoutine = false;
    public int health = 5;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 rotation = transform.localEulerAngles;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rotation.z += Time.deltaTime * anglePerSecond;
            transform.localEulerAngles = rotation;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rotation.z -= Time.deltaTime * anglePerSecond;
            transform.localEulerAngles = rotation;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!playerHitRoutine) { 
                StartCoroutine(Swim());
            }
            //anim.Play("playerswim");
            rb.AddForce(transform.up * swimForce, ForceMode2D.Impulse);
        }

        if (health <= 0) 
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            /*Vector2 direction = (transform.position - col.transform.position).normalized;
            Vector2 knockback = direction * knockBackForce;
            rb.AddForce(knockback, ForceMode2D.Impulse);
            StartCoroutine(hit());*/
            health--;
            anim.Play("playerHit");
            StartCoroutine("playerHit");
        }
    }

    private IEnumerator playerHit() 
    {
        playerHitRoutine = true;
        Physics2D.IgnoreLayerCollision(6, 8, true);
  
        yield return new WaitForSeconds(3f);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        anim.Play("playerIdle");
        playerHitRoutine = false;
    }

    private IEnumerator Swim() 
    {
        anim.Play("playerswim");
        yield return new WaitForSeconds(.35f);
        anim.Play("playerIdle");
    }
}
