using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnBoundsX = 9f;
    [SerializeField] private float spawnBoundsY = 7f;

    [SerializeField] private float enemySpawnRate = 5f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyContainer;

    [SerializeField] private float powerupSpawnRateLower = 5f;
    [SerializeField] private float powerupSpawnRateUpper = 10f;
    [SerializeField] private GameObject[] powerupPrefabs;
    [SerializeField] private Transform powerupContainer;

    private Coroutine runningEnemyRoutine;
    private Coroutine runningPowerupRoutine;

    private void Start()
    {
        if(enemyPrefab == null)
        {
            Debug.LogError("Object To Spawn is not attached.");
        }
        else if(enemyContainer == null)
        {
            Debug.LogError("Object Parent Container is not attached.");
        }
        else
        {
            runningEnemyRoutine = StartCoroutine(EnemySpawnRoutine(enemySpawnRate));
        }

        if(powerupPrefabs.Length <= 0)
        {
            Debug.LogError("Powerup Prefabs are not set.");
        }
        else if(powerupContainer == null)
        {
            Debug.LogError("Powerup Prefab container is NULL");
        }
        else
        {
            runningPowerupRoutine = StartCoroutine(PowerupSpawnRoutine());
        }
    }

    IEnumerator EnemySpawnRoutine(float spawnRate)
    {
        while (true)
        {

            var spawnPosition = new Vector3(Random.Range(-spawnBoundsX, spawnBoundsX), spawnBoundsY, 0);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemyContainer);
            yield return new WaitForSeconds(spawnRate);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (true)
        {
            int powerupIndex = Random.Range(0, powerupPrefabs.Length);
            if (powerupPrefabs[powerupIndex] != null)
            {
                var spawnPosition = new Vector3(Random.Range(-spawnBoundsX, spawnBoundsX), spawnBoundsY, 0);
                Instantiate(powerupPrefabs[powerupIndex], spawnPosition, Quaternion.identity, powerupContainer);
            }
            
            yield return new WaitForSeconds(Random.Range(powerupSpawnRateLower, powerupSpawnRateUpper));
        }
    }

    public void OnPlayerDeath()
    {
        StopCoroutine(runningEnemyRoutine);
        StopCoroutine(runningPowerupRoutine);
    }
}
