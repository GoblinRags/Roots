using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Root : MonoBehaviour
{
    public Vector3 EndPoint { get; set; }
    public Transform EndTransform;

    [HideInInspector]public StartingRoot startingRoot;
    [SerializeField]private BoxCollider2D col;

    [SerializeField] private RootPhysics physics;

    [SerializeField] private float rand = .5f;

    public void TurnOffRb()
    {
        //col.enabled = false;
        
    }

    public void TurnOnPhysics()
    {
        physics.enabled = true;
    }

    public void TeleportAndRotate()
    {
        transform.position += new Vector3(Random.Range(-rand, rand), Random.Range(-rand, rand));
        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360f));
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Detected");
        if (col.gameObject.CompareTag("Player"))
        {
            startingRoot.WasHit();
        }

        if (col.gameObject.CompareTag("Finish"))
        {
            if (startingRoot.WasCut)
            {
                Destroy(gameObject);
            }
            else
            {
                //game over
                startingRoot.HitCenter();
            }
            
        }
    }
    
    
}
