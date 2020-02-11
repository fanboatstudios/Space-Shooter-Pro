using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LivesDisplay : MonoBehaviour
{
    [SerializeField] private Sprite[] livesSprites;

    private Image livesDisplay;

    void Awake()
    {
        livesDisplay = GetComponent<Image>();
        if(livesDisplay == null)
        {
            Debug.LogError("Lives Display is NULL");
        }


        if(livesSprites == null)
        {
            Debug.LogError("LivesSprites array is empty!");
        }
    }

    public void UpdateLives(int livesRemaning)
    {
        if (livesRemaning < 0) livesRemaning = 0;
        if(livesSprites[livesRemaning] != null)
        {
            livesDisplay.sprite = livesSprites[livesRemaning];
        }
    }
}
