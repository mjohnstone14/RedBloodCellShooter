using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Main menu.
/// Control the Main Menu scene and load other scenes.
/// </summary>
public class MainMenu : MonoBehaviour {

	public string startLevelName;

	public void StartGame() {
		Time.timeScale = 1;
		SceneManager.LoadScene (startLevelName);
	}

	public void Credits() {
		SceneManager.LoadScene ("Credits");
	}
	public void Instructions() {
		SceneManager.LoadScene ("Instructions");
	}

	public void GoToMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}

}
