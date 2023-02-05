using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;


    private int score;
    [SerializeField]private TextMeshProUGUI scoreText;

    //[SerializeField] private TextMeshProUGUI gameOverText;

    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    [SerializeField] private GameObject gameOverObject;

    private bool _isGameOver = false;
    [SerializeField] private PlayerController pc;
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

    public void GameOver()
    {
        //gameOverText.gameObject.SetActive(true);
        scoreText.text = "";
        gameOverScoreText.text = score.ToString();
        gameOverScoreText.gameObject.SetActive(true);
        gameOverObject.SetActive(true);
        _isGameOver = true;
        pc.enabled = false;
    }

    public void Update()
    {
        if (_isGameOver && Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
