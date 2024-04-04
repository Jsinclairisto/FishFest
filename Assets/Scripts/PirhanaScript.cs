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
    private int pirhanaHealth = 3;

    Vector2 waypoint;

    void Start()
    {
        SetNewDestination();
        speed = Random.Range(5, 10);
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypoint) < range) 
        {
            SetNewDestination();
        }
        if (pirhanaHealth == 0) 
        {
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
            pirhanaHealth--;
        }
    }


}
