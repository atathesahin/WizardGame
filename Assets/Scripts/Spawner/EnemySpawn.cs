using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  
    public int numberOfEnemiesToSpawn = 10;  
    public float spawnInterval = 2f;  
    public Vector3 spawnAreaSize = new Vector3(20f, 1f, 20f);  
    public string nextSceneName;
    private int enemiesSpawned = 0;
    private int sceneNumber = 1;  

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < numberOfEnemiesToSpawn)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

         
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            enemiesSpawned++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
        float y = spawnAreaSize.y / 2f;
        float z = Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f);

        return new Vector3(x, y, z);
    }

    void Update()
    {
        if (enemiesSpawned >= numberOfEnemiesToSpawn)
        {
            sceneNumber++;
            StartCoroutine(ChangeSceneAfterDelay(15f));
        }
    }

    IEnumerator ChangeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }

}

