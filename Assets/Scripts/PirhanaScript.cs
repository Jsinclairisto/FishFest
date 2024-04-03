using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirhanaScript : MonoBehaviour
{
    [SerializeField]
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

    Vector2 waypoint;

    void Start()
    {
        SetNewDestination();    
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypoint) < range) 
        {
            SetNewDestination();
        }
    }

    void SetNewDestination() 
    {
        waypoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

}
