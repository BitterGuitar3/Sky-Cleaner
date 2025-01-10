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
   // public AudioClip explosion;
    //private int explosionCount = 0;
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

    // Update is called once per frame
    void Update()
    {
        /*if(GameObject.FindGameObjectsWithTag("Player").Length == 0 && explosionCount == 0)
        {
            gameAudio.PlayOneShot(explosion, 0.25f);
            explosionCount++;
        }*/
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score; 
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

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
