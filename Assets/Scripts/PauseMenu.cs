using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Pause menu.
/// Manages pause menu.
/// </summary>
public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenuCanvas;

	// Use this for initialization
	void Start () {
		GameManager.gameState = GameManager.GameState.Running;
		pauseMenuCanvas.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameManager.gameState = GameManager.GameState.Paused;
			Pause ();
		}
	}
	/// <summary>
	/// Triggers pause menu
	/// </summary>
	public void Pause() {
		GameManager.gameState = GameManager.GameState.Paused;
		pauseMenuCanvas.SetActive (true);
		Time.timeScale = 0;
	}

	/// <summary>
	/// Resumes game.
	/// </summary>
	public void Resume() {
		if (Input.GetKeyDown (KeyCode.Space))
			pauseMenuCanvas.SetActive (true);
		else {
			pauseMenuCanvas.SetActive (false);
			GameManager.gameState = GameManager.GameState.Running;
			Time.timeScale = 1;
		}
	}

	/// <summary>
	/// Returns to main menu.
	/// </summary>
	public void ReturnToMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}
}
