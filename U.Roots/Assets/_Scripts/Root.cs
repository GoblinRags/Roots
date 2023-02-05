using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public Vector3 EndPoint { get; set; }
    public Transform EndTransform;

    public StartingRoot startingRoot;


    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Detected");
        if (col.gameObject.CompareTag("Player"))
        {
            startingRoot.WasHit();
            Debug.Log("Was hit");
        }

        if (col.gameObject.CompareTag("Finish"))
        {
            
        }
    }
    
    
}
