using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shoot();
        }
    }
    void Shoot() 
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}
