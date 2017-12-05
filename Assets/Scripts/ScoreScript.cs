using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Score script.
/// Class initializes score value and contains a method that adds points to it.
/// </summary>
public class ScoreScript : MonoBehaviour {

	public static int score;
	Text scoreNum;
	Text scoreDisplay;
	// Use this for initialization
	void Start () {
		scoreNum = GameObject.FindGameObjectWithTag("score").GetComponent<Text> (); 
		score = 0;
	}

	void Update()
	{
		scoreNum.text = "" + score;
	}
		
	/// <summary>
	/// Adds points when called.
	/// </summary>
	public static void AddPoints()
	{
		score += 10;
	}

}
