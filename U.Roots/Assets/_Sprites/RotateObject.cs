using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float Speed;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.back, Time.fixedDeltaTime * Speed);
    }
}
