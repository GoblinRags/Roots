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


    private float numberOfSpawns = 1f;
    private float startSpawnTime = 5f;
    private float _timer = 0f;
    private float phase = 2;

    private float rootChildSpawnTime = 1f;


    private float _spawnTimer = 0f;
    private float _spawnTime = 5f;
    private void Awake()
    {
        SpawnObject();
        SpawnObject();
        SpawnObject();
    }


    private void Update()
    {
        _timer += Time.deltaTime;

        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnTime)
        {
            SpawnObject();
        }


        if (_timer >= 60f)
        {
            NextPhase();
            _timer = 0f;
        }
    }

    public void NextPhase()
    {
        phase += 1;
        _spawnTime = startSpawnTime / (float)Math.Log10(phase);
        numberOfSpawns = phase;
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
        
        //variables
        startingRoot._spawnTime = rootChildSpawnTime;
        
        
        rootManager.AddRoot(startingRoot);
    }
}

