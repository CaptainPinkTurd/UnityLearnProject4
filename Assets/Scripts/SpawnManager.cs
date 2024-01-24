using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] GameObject[] powerUpPrefab;
    private float spawnRange = 9;
    public int enemyCount;
    private int numberOfEnemiesToSpawn = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemiesWave(numberOfEnemiesToSpawn);
        SpawnPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        //counting number of enemies on the screen
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            numberOfEnemiesToSpawn++;
            SpawnPowerUp();
            SpawnEnemiesWave(numberOfEnemiesToSpawn);
        }
    }
    private void SpawnEnemiesWave(int EnemyToSpawn)
    {
        int enemyType;

        for(int i = 0; i < EnemyToSpawn; i++)
        {
            int chances = Random.Range(0, 100);

            //choosing which enemy type to spawn depending on the chance
            enemyType = chances <= 75 ? 0 : 1;

            Instantiate(enemyPrefab[enemyType], GenerateSpawnPosition(), Quaternion.identity);
        }
    }
    private void SpawnPowerUp()
    {
        int chances = Random.Range(0, 90);
        int powerUpType = chances < 30 ? 0 : chances >= 60 ? 1 : 2;

        Instantiate(powerUpPrefab[powerUpType], GenerateSpawnPosition(), Quaternion.identity);
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(spawnRange, -spawnRange);
        float spawnPosZ = Random.Range(spawnRange, -spawnRange);

        Vector3 spawnPos = new Vector3(spawnPosX, 0.3f, spawnPosZ);

        return spawnPos;
    }
}
