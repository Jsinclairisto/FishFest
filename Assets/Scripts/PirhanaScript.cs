using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirhanaScript : MonoBehaviour
{
    private float speed;
    [SerializeField]
    private float range;
/*    [SerializeField]
    private float maxDistance;*/
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;

    [SerializeField]
    private float knockBackForce;

    [SerializeField]
    private int pirhanaHealth = 3;
    private ShakeManager shake;
    private bool flip;
    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    public GameObject deathEffect;
    SoundManager soundManager;
    Vector2 waypoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        //SetNewDestination();
        speed = Random.Range(5, 8);
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();

        anim = GetComponent<Animator>();
        shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<ShakeManager>();

    }
    void FixedUpdate()
    {
        /*transform.position = Vector2.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypoint) < range) 
        {
            SetNewDestination();
        }
        */
       

        if (pirhanaHealth == 0) 
        {
            soundManager.PlaySFX(soundManager.death);
            shake.CamShake();
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        Vector3 scale = transform.localScale;
        if (player.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            //transform.Rotate(new Vector3(0, 0, 0));
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            //transform.Rotate(new Vector3(0, 180, 0));
        }
        transform.localScale = scale;
    }

    /*void SetNewDestination()
    {
        waypoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
*/
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("bullet")) 
        {
            
            Vector2 direction = (transform.position - col.transform.position).normalized;
            Vector2 knockback = direction * knockBackForce;
            rb.AddForce(knockback, ForceMode2D.Impulse);
            StartCoroutine(hit());
            pirhanaHealth--;
        }
    }

    private IEnumerator hit() 
    {
        anim.Play("Hit");
        yield return new WaitForSeconds(.1f);
        anim.Play("Idle");
    }

}
