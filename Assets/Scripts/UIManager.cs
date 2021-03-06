﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private LivesDisplay livesDisplay;
    private GameOver gameOverText;
    
    private void Awake()
    { 
        gameOverText = FindObjectOfType<GameOver>();
        if(gameOverText == null)
        {
            Debug.LogError("GameOverText is NULL");
        }
        else
        {
            gameOverText.enabled = false;
        }

        livesDisplay = FindObjectOfType<LivesDisplay>();
        if(livesDisplay == null)
        {
            Debug.LogError("LivesDisplay is NULL");
        }

        if(scoreText == null)
        {
            Debug.LogError("Score Text is NULL!");
        }
        else
        {
            scoreText.text = "Score: 0";
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void UpdateLives(int livesRemaining)
    {
        livesDisplay.UpdateLives(livesRemaining);
    }

    public void EnableGameOver()
    {
        gameOverText.GameIsOver();
    }
}
