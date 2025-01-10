using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//This script deals with spawning everything needed in the game at their respective times. Deletion and destruction of objects will be dealt with by their own scripts
public class SpawnManager : MonoBehaviour
{
    public GameObject[] basicEnemyPrefabs;
    public GameObject[] bigEnemyPrefabs;
    public GameObject balloon;

    public GameObject[] trees;
    public GameObject[] clouds;

    private GameManager gameManager;

    private float spawnPosZ = 18.0f;
    private float treeSpawnz = 15.0f;
    private float spawnBasicRangeX = 20.0f;
    private float spawnRareRangeX = 13.5f;
    private float treeRangeX = 32.0f;

    public float difficulty;

    private float basicSpawnRate = 1.0f;
    private float rareSpawnRate = 7.5f;
    private float balloonSpawnRate = 2.0f;
    private float treeSpawnRate = 0.35f;
    private float cloudSpawnRate = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    
    //Spawns the basic small enemies at regular intervals based on game difficulty
    public IEnumerator SpawnBasics()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(basicSpawnRate / difficulty);
            SpawnRandomBasic();
        }
    }

    //Spawns the big groups of enemies at regular intervals based on game difficulty. The plane that i can spawn is actually a civillian
    public IEnumerator SpawnRares()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(rareSpawnRate / difficulty);
            SpawnRandomRare();
        }
    }

    public IEnumerator SpawnBalloons()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(balloonSpawnRate);
            SpawnRandomBalloon();
        }
    }
    //The 3 IEnumerators below are fore more background decoration spawning. Gives a sense of movement
    public IEnumerator SpawnTrees()
    {
        while (true)
        {
            yield return new WaitForSeconds(treeSpawnRate);
            SpawnTwoTrees();
        }
    }

    public IEnumerator SpawnRightClouds()
    {
        while (true)
        {
            yield return new WaitForSeconds(cloudSpawnRate);
            float xPos = 29.0f;
            SpawnCloud(xPos);
        }
    }

    public IEnumerator SpawnLeftClouds()
    {
        while (true)
        {
            yield return new WaitForSeconds(cloudSpawnRate);
            float xPos = -29.0f;
            SpawnCloud(xPos);
        }
    }


    void SpawnRandomBasic()
    {
        int prefabIndex = Random.Range(0, basicEnemyPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnBasicRangeX, spawnBasicRangeX), 1, spawnPosZ);

        Instantiate(basicEnemyPrefabs[prefabIndex], spawnPos, basicEnemyPrefabs[prefabIndex].transform.rotation);
    }

    void SpawnRandomRare()
    {
        int prefabIndex = Random.Range(0, bigEnemyPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRareRangeX, spawnRareRangeX), 1, spawnPosZ);

        Instantiate(bigEnemyPrefabs[prefabIndex], spawnPos, bigEnemyPrefabs[prefabIndex].transform.rotation);
    }

    void SpawnRandomBalloon()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnBasicRangeX, spawnBasicRangeX), 1, spawnPosZ);
        Instantiate(balloon, spawnPos, balloon.transform.rotation);
    }

    void SpawnTwoTrees()
    {
        int treeNum = Random.Range(0, trees.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-treeRangeX, treeRangeX), -99, treeSpawnz);
        Instantiate(trees[treeNum], spawnPos, trees[treeNum].transform.rotation);
    }

    void SpawnCloud(float xPos)
    {
        int cloudNum = Random.Range(0, clouds.Length);
        Vector3 spawnPos = new Vector3(xPos, 0, 40);
        Instantiate(clouds[cloudNum], spawnPos, clouds[cloudNum].transform.rotation);
    }
}
