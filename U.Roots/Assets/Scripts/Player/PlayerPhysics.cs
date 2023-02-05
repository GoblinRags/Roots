using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Transform center;

    [Space(10)]
    [SerializeField] private float gravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        var downDirection = center.position - transform.position;
        rb.AddForce(downDirection * 9.81f * gravityScale);
        transform.up = -downDirection;
    }
}
