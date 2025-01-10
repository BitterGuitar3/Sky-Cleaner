using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    private SpawnManager spawnManager;
    public AudioSource gameAudio;
    private int score;
    public bool isGameActive;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        gameAudio = GetComponent<AudioSource>();
        StartCoroutine(spawnManager.SpawnTrees());
        StartCoroutine(spawnManager.SpawnRightClouds());
        StartCoroutine(spawnManager.SpawnLeftClouds());
    }

    //Updates the score and the score text
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score; 
    }
    
    //Ends the game and stops the coroutines.
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    //Coroutines continue as long as isGameActive is true
    public void StartGame(float difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnManager.difficulty = difficulty;
        UpdateScore(0);
        titleScreen.SetActive(false);
        StartCoroutine(spawnManager.SpawnBasics());
        StartCoroutine(spawnManager.SpawnRares());
        StartCoroutine(spawnManager.SpawnBalloons());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
