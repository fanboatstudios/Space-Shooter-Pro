using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region IsGameOver property

	[SerializeField] private bool isGameOver;

    public bool IsGameOver
    {
        get { return isGameOver; }
        private set { isGameOver = value; }
    }

    #endregion

	private Scene currentScene;
    private UIManager uiManager;
    private SpawnManager spawnManager;

    [SerializeField] private float reductionRate = .25f;
    [SerializeField] private float spawnModificationRate = 20f;
    private WaitForSeconds spawnModificationWaitTime;

    private void Awake()
    {
        spawnModificationWaitTime = new WaitForSeconds(spawnModificationRate);

        spawnManager = FindObjectOfType<SpawnManager>();
        if(spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }

        uiManager = FindObjectOfType<UIManager>();
        if(uiManager == null)
        {
            Debug.LogError("UIManager is NULL!");
        }

        currentScene = SceneManager.GetActiveScene();
        if (!currentScene.IsValid())
        {
            Debug.LogError("Current Scene is not valid.");
        }
    }

    private void Update()
    {
        CheckForQuit();
        CheckForRestart();
    }

    private void CheckForQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void CheckForRestart()
    {
        if (Input.GetKeyDown(KeyCode.R) && IsGameOver)
        {
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        uiManager.EnableGameOver();
    }

    public void EnableSpawning()
    {
        spawnManager.EnableSpawning();
        StartCoroutine(EnemySpawnRateModifierRoutine());
    }

    IEnumerator EnemySpawnRateModifierRoutine()
    {
        while (true)
        {
            yield return spawnModificationWaitTime;
            spawnManager.ReduceEnemySpawnRateBy(reductionRate);
        }
    }
}
