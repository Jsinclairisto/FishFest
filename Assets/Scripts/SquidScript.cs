using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidScript : MonoBehaviour
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
    private float waitTime;
    private float startWaitTime;
    private Transform moveSpot;
    [SerializeField]
    private float knockBackForce;
    SoundManager soundManager;
    Vector2 waypoint;
    [SerializeField]
    private int squidHealth = 5;
    PlayerMovement playerMovement;
    public GameObject projectile;
    private float timeBtwShots;
    public float startTimeBtwShots;
    private Transform player;
    //private Transform player;
    private ShakeManager shake;
    private Animator anim;
    private Rigidbody2D rb;
    public GameObject squidDeathEffect;
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        moveSpot = GameObject.FindWithTag("moveSpot").transform;
        rb = GetComponent<Rigidbody2D>();
        waitTime = startWaitTime;
        speed = Random.Range(5, 10);
        anim = GetComponent<Animator>();
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<ShakeManager>();
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
        SetNewDestination();
        speed = Random.Range(5, 8);

    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypoint) < range)
        {
            /*if (waitTime <= 0)
            {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }
            else 
            {
                waitTime -= Time.deltaTime;
            }*/
            SetNewDestination();
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else 
        {
            timeBtwShots -= Time.deltaTime;
        }
        
        if (squidHealth == 0)
        {
            playerMovement.Score += 75;
            soundManager.PlaySFX(soundManager.death);
            shake.CamShake();
            Instantiate(squidDeathEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    void SetNewDestination()
    {
        waypoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("bullet"))
        {
            Vector2 direction = (transform.position - col.transform.position).normalized;
            Vector2 knockback = direction * knockBackForce;
            rb.AddForce(knockback, ForceMode2D.Impulse);
            StartCoroutine(hit());
            squidHealth--;
        }
    }

    private IEnumerator hit()
    {
        anim.Play("SquidHit");
        yield return new WaitForSeconds(.1f);
        anim.Play("squididle");
    }
}
