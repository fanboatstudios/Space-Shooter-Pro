using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private void Start()
    {
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

}
