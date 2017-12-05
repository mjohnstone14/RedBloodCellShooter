using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Virus count.
/// Display the number of viruses killed during the game in the trophy scenes.
/// </summary>
public class VirusCount : MonoBehaviour {
	public Text virusCount;
	int score = ScoreScript.score;
	// Use this for initialization
	void Start () {
		virusCount = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		int numVirus = score / 10;
		virusCount.text = numVirus.ToString ();
	}
}
