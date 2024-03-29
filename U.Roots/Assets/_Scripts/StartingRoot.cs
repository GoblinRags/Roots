using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class StartingRoot : MonoBehaviour
{
    public GameObject[] RootPrefabs;
    public GameObject EnemyPrefab;
    //public Transform EnemyPosition;
    private GameObject Enemy;
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

    private AudioManager am;

    private void Awake()
    {
        //Root root = Instantiate(EnemyPrefab, EnemyPosition.transform.position, Quaternion.Euler(0f, 0f, 0f),
        //    Holder).GetComponent<Root>();
        //root.gameObject.transform.localRotation = quaternion.Euler(0f, 0f, 0f);
    }


    private void Start()
    {
        SpawnRoot(true);
        //am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    

    public void Update()
    {
        if (!IsSpawning || WasCut || HasHitCenter)
            return;
        _timer += Time.deltaTime;
        if (_timer >= _spawnTime && IsSpawning)
        {
            _timer = 0f;
            SpawnRoot();
        }
    }


    public void SpawnRoot(bool isStart = false)
    {
        //Root root = Instantiate(RootPrefabs[Random.Range(0, RootPrefabs.Length)], transform, );
        Root root = Instantiate(RootPrefabs[Random.Range(0, RootPrefabs.Length)], _currentPoint, Quaternion.Euler(0f, 0f, 0f),
            Holder).GetComponent<Root>();
        root.gameObject.transform.localRotation = quaternion.Euler(0f, 0f, 0f);
        root.startingRoot = this;
        if (isStart)
        {
            GameObject obj = Instantiate(EnemyPrefab, _currentPoint + new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f),
                Holder);
            obj.transform.localRotation = quaternion.Euler(0f, 0f, 0f);
            Enemy = obj;
        }
        
        
        _currentPoint = root.EndTransform.transform.position;
        roots.Add(root);
        //root.gameObject.transform.eulerAngles = new Vector3(0f, 0f, _angle);

    }

    public void WasHit()
    {
        
        GameManagerScript.Instance.AddScore(roots.Count);
        if (WasCut)
            return;
        
        Destroy(Enemy);
        IsSpawning = false;
        WasCut = true;
        
        foreach (Root root in roots)
        {
            //root.TurnOffRb();
            root.TurnOnPhysics();
            root.TeleportAndRotate();
        }

        var randAud = Random.Range(0f, 1f);
        if (randAud < .33f)
        {
            AudioManager.Instance.PlaySfx(AudioManager.Sound.Hit1);
        }
        else if (randAud < .66f)
        {
            AudioManager.Instance.PlaySfx(AudioManager.Sound.Hit2);
        }
        else
        {
            AudioManager.Instance.PlaySfx(AudioManager.Sound.Hit3);
        }

        StartCoroutine(PlayBreaking(.05f));
        StartCoroutine(PlayShrivel(.15f));
    }

    public IEnumerator PlayShrivel(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySfx(AudioManager.Sound.Shrivelling, .05f);
        
    }
    
    public IEnumerator PlayBreaking(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySfx(AudioManager.Sound.Breaking, .05f);
        
    }
    
    public void HitCenter()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Sound.Shrivelling, .05f);
        AudioManager.Instance.PlaySfx(AudioManager.Sound.Breaking, .05f);
        HasHitCenter = true;
        GameManagerScript.Instance.GameOver();
        foreach (Root root in roots)
        {
            Destroy(root.gameObject);
        }
    }
}
