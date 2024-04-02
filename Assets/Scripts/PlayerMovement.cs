using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float anglePerSecond;
    [SerializeField] private float swimForce;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            rb.AddForce(transform.up * swimForce, ForceMode2D.Impulse);
        }
    }
}
