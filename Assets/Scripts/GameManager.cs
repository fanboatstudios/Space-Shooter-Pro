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

    private void Awake()
    {
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
}
