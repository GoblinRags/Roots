using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;


    private int score;
    [SerializeField]private TextMeshProUGUI scoreText;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public void AddScore(int add)
    {
        score += add;
        scoreText.text = score.ToString();
    }
}
