using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float minimumSpawnTime;
    
    [SerializeField]
    private float maximumSpawnTime;

    private float timeUntilSpawn;
    
    // Start is called before the first frame update
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0) 
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
        if (minimumSpawnTime > 5 && maximumSpawnTime > 5) 
        {
            minimumSpawnTime -= 0.0009f;
            maximumSpawnTime -= 0.0009f;
        }

    }
    private void SetTimeUntilSpawn() 
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}
