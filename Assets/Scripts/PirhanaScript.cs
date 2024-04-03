using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirhanaScript : MonoBehaviour
{
    public float speed;

    public Transform moveSpot;
    private float waitTime;
    public float startWaitTime;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    void Start()
    {
        waitTime = startWaitTime;
        
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }
            else 
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
