using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class RootSpawner : MonoBehaviour
{
    [SerializeField] private float radius;

    [SerializeField] public GameObject startRootPrefab;

    [SerializeField] private RootManager rootManager;

    public Transform center;
    private void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            SpawnObject();
        }
    }

    [SerializeField] private Vector2 Center;
    
    public void SpawnObject()
    {
        //Vector3 pos = new Vector3(0f, radius, 0f);
        Vector2 randPos = Center + (Random.insideUnitCircle.normalized * radius);
        float angle = Vector2.SignedAngle(new Vector2(0, 1), randPos - Center);
        
        StartingRoot startingRoot = Instantiate(startRootPrefab, rootManager.transform).GetComponent<StartingRoot>();
        //startingRoot.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 180);
        startingRoot.Holder.eulerAngles = new Vector3(0f, 0f, angle);
        startingRoot._currentPoint = randPos;
        startingRoot._angle = angle;
        startingRoot.rootSpawner = this;
        rootManager.AddRoot(startingRoot);
    }
}

