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

    private Transform player;

    private Rigidbody2D rb;
    Vector2 waypoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        //SetNewDestination();
        speed = Random.Range(5, 10);

    }
    void Update()
    {
        /*transform.position = Vector2.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypoint) < range) 
        {
            SetNewDestination();
        }
        */
        if (pirhanaHealth == 0) 
        {
            Destroy(this.gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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
            pirhanaHealth--;
        }
    }


}
