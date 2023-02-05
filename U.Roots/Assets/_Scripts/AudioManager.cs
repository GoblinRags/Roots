using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource AS;
    public enum Sound {Breaking, ConstantWalking, Hit1, Hit2, Hit3, Shrivelling, Slash, SlashWind, TwoSteps, Slash2}
    public AudioClip[] sfx;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }
    
    public void PlaySfx(Sound type, float volume = 1f)
    {
        AS.PlayOneShot(sfx[(int)type], volume);
    }
}
