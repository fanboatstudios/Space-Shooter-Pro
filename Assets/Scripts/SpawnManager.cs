using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private float enemyBoundsX = 9f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyContainer;

    private Coroutine runningRoutine;

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
            runningRoutine = StartCoroutine(SpawnRoutine(spawnRate));
        }
    }

    IEnumerator SpawnRoutine(float spawnRate)
    {
        while (true)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-enemyBoundsX, enemyBoundsX), 7f, 0), Quaternion.identity, enemyContainer);
            yield return new WaitForSeconds(spawnRate);
        }
    }

    public void OnPlayerDeath()
    {
        StopCoroutine(runningRoutine);
    }
}
