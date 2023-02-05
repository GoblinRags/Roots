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
    public float _spawnTime = 1f;
    public Vector3 _currentPoint = new Vector3();
    public float _angle = 0f;
    public RootSpawner rootSpawner;
    private List<Root> roots = new List<Root>();

    private bool IsSpawning = true;
    public bool WasCut = false;
    private bool HasHitCenter = false;
    private void Start()
    {
        SpawnRoot();
    }

    

    public void Update()
    {
        if (!IsSpawning)
            return;
        _timer += Time.deltaTime;
        if (_timer >= _spawnTime && IsSpawning)
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
        
        roots.Add(root);
        //root.gameObject.transform.eulerAngles = new Vector3(0f, 0f, _angle);

    }

    public void WasHit()
    {
        IsSpawning = false;
        WasCut = true;
        GameManagerScript.Instance.AddScore(1);
        foreach (Root root in roots)
        {
            //root.TurnOffRb();
            root.TurnOnPhysics();
            root.TeleportAndRotate();
        }

    }

    public void HitCenter()
    {
        HasHitCenter = true;
        foreach (Root root in roots)
        {
            Destroy(root.gameObject);
        }
    }
}
