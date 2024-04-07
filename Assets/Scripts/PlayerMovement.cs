using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float anglePerSecond;
    [SerializeField] private float swimForce;
    private Animator anim;
    private Rigidbody2D rb;
    public GameObject playerDeath;
    public bool playerHitRoutine = false;
    public int health = 5;
    SoundManager soundManager;
    private ShakeManager shake;
    Renderer rend;
    Color c;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
        c = rend.material.color;
        shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<ShakeManager>();
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
            soundManager.PlaySFX(soundManager.swim);
            StartCoroutine(Swim());
            //anim.Play("playerswim");
            rb.AddForce(transform.up * swimForce, ForceMode2D.Impulse);
        }
        if (health <= 0) 
        {
            soundManager.PlaySFX(soundManager.death);
            shake.CamShake();
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(playerDeath, transform.position, transform.rotation);
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
            soundManager.PlaySFX(soundManager.bulletHit);
            shake.CamShake();
            health--;
            StartCoroutine("playerHit");
        }
    }

    private IEnumerator playerHit() 
    {
        playerHitRoutine = true;
        Physics2D.IgnoreLayerCollision(6, 8, true);
        c.a = 0f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 1f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 0f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 1f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 0f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 1f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 0f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 1f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 0f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 1f;
        rend.material.color = c;
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
