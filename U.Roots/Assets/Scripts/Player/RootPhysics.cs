using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootPhysics : MonoBehaviour
{
    private Rigidbody2D rb;
    [HideInInspector] private Transform center;

    [Space(10)]
    [SerializeField] private float gravityScale;

    [SerializeField] private Root root;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        center = root.startingRoot.rootSpawner.center;
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