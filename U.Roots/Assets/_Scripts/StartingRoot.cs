using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class StartingRoot : MonoBehaviour
{
    public GameObject[] RootPrefabs;


    public Transform Holder;
    private float _timer = 0f;
    private float _spawnTime = 1f;
    public Vector3 _currentPoint = new Vector3();
    public float _angle = 0f;
    
    private void Start()
    {
        SpawnRoot();
    }


    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnTime)
        {
            _timer = 0f;
            SpawnRoot();
        }
    }


    public void SpawnRoot()
    {
        //Root root = Instantiate(RootPrefabs[Random.Range(0, RootPrefabs.Length)], transform, );
        Root root = Instantiate(RootPrefabs[Random.Range(0, RootPrefabs.Length)], _currentPoint, Quaternion.Euler(0f, 0f, 0f),
            Holder).GetComponent<Root>();
        root.gameObject.transform.localRotation = quaternion.Euler(0f, 0f, 0f);
        _currentPoint = root.EndTransform.transform.position;
        root.startingRoot = this;
        //root.gameObject.transform.eulerAngles = new Vector3(0f, 0f, _angle);

    }

    public void WasHit()
    {
        //destroy object
        
        
    }
}