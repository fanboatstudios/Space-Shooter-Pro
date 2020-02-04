using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnBoundsX = 9f;
    [SerializeField] private float spawnBoundsY = 7f;

    [SerializeField] private float enemyLowerSpawnRate = .5f;
    [SerializeField] private float currentEnemySpawnRate = 5f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyContainer;

    [SerializeField] private float powerupSpawnRateLower = 5f;
    [SerializeField] private float powerupSpawnRateUpper = 10f;
    [SerializeField] private GameObject[] powerupPrefabs;
    [SerializeField] private Transform powerupContainer;

    private Coroutine runningEnemyRoutine;
    private Coroutine runningPowerupRoutine;
    private WaitForSeconds enemySpawnWaitForSeconds;

    [SerializeField] float startDelay = 1f;
    private WaitForSeconds startDelayWaitForSeconds;



    private void Start()
    {
        enemySpawnWaitForSeconds = new WaitForSeconds(currentEnemySpawnRate);
        startDelayWaitForSeconds = new WaitForSeconds(startDelay);

        if (enemyPrefab == null)
        {
            Debug.LogError("Object To Spawn is not attached.");
        }
        else if(enemyContainer == null)
        {
            Debug.LogError("Object Parent Container is not attached.");
        }

        if(powerupPrefabs.Length <= 0)
        {
            Debug.LogError("Powerup Prefabs are not set.");
        }
        else if(powerupContainer == null)
        {
            Debug.LogError("Powerup Prefab container is NULL");
        }
    }

    IEnumerator EnemySpawnRoutine()
    {
        yield return startDelayWaitForSeconds;
        while (true)
        {
            var spawnPosition = new Vector3(Random.Range(-spawnBoundsX, spawnBoundsX), spawnBoundsY, 0);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemyContainer);
            yield return enemySpawnWaitForSeconds;
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        yield return startDelayWaitForSeconds;
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

    public void EnableSpawning()
    {
        runningEnemyRoutine = StartCoroutine(EnemySpawnRoutine());
        runningPowerupRoutine = StartCoroutine(PowerupSpawnRoutine());
    }

    public void ReduceEnemySpawnRateBy(float reductionRate)
    {
        if (currentEnemySpawnRate > enemyLowerSpawnRate)
        {
            currentEnemySpawnRate -= reductionRate;
            enemySpawnWaitForSeconds = new WaitForSeconds(currentEnemySpawnRate);
        }
    }
}
