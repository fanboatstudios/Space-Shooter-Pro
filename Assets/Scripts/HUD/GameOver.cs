using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	[SerializeField] private float flashTime = 1f;
	private Text gameOverText;
	private Text restartInstructionsText;
	private WaitForSeconds flashWaitTime;

	private void Awake()
	{
		gameOverText = transform.GetChild(0).GetComponent<Text>();
		if (gameOverText == null)
		{
			Debug.LogError("GameOverText is NULL");
		}
		else
		{
			gameOverText.enabled = false;
		}

		restartInstructionsText = transform.GetChild(1).GetComponent<Text>();
		if (restartInstructionsText == null)
		{
			Debug.LogError("RestartInstructionsText is NULL");
		}
		else
		{
			restartInstructionsText.enabled = false;
		}

		flashWaitTime = new WaitForSeconds(flashTime);
		
	}

	public void GameIsOver()
	{
		restartInstructionsText.enabled = true;
		StartCoroutine(FlashGameOverRoutine());
	}

	IEnumerator FlashGameOverRoutine()
	{
		while (true)
		{
			gameOverText.enabled = true;
			yield return flashWaitTime;

			gameOverText.enabled = false;
			yield return flashWaitTime;
		}
	}
}
