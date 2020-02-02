using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
	public void LoadSinglePlayerGame()
	{
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}

	private void Update()
	{
		CheckForQuit();
	}

	private void CheckForQuit()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
