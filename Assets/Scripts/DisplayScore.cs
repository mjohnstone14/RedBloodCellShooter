using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Display score.
/// Show the final score on the trohpy scences.
/// </summary>
public class DisplayScore : MonoBehaviour {
	public Text scoreText;
	int score = ScoreScript.score;
	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = score.ToString ();
	}
}
