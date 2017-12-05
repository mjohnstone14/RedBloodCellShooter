using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public enum GameState
	{
		MainMenu,
		Running,
		Paused
	}
	public static GameState gameState;



	// Use this for initialization
	void Start () {
		gameState = GameState.Running;
	}

	// Update is called once per frame
	void Update () {
	}

	public static void Quit() {
		Application.Quit ();	
	}
}