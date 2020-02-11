using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
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

    [SerializeField] private float reductionRate = .25f;
    [SerializeField] private float spawnModificationRate = 20f;
    private WaitForSeconds spawnModificationWaitTime;

    public override void Init()
    {
        base.Init();

        spawnModificationWaitTime = new WaitForSeconds(spawnModificationRate);

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
        UIManager.Instance.EnableGameOver();
    }

    public void EnableSpawning()
    {
        SpawnManager.Instance.EnableSpawning();
        StartCoroutine(EnemySpawnRateModifierRoutine());
    }

    IEnumerator EnemySpawnRateModifierRoutine()
    {
        while (true)
        {
            yield return spawnModificationWaitTime;
            SpawnManager.Instance.ReduceEnemySpawnRateBy(reductionRate);
        }
    }
}
