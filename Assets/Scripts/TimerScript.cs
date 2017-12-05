using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
/// <summary>
/// Timer Script.
/// Class that creates a timer, casts it into an int and displays it to the screen as a string used in the game scene for the timer UI.
/// </summary>
public class TimerScript : MonoBehaviour {
	public Text timerText;
	private float startTime;
	public string minutes;
	public string seconds;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float t = Time.time - startTime; //value that tracks the time from start

		minutes = ((int) t / 60).ToString ();
		seconds = (t % 60).ToString("f0");

		timerText.text = minutes + ":" + seconds; //parsing data into string to be displayed to UI
	}
}
