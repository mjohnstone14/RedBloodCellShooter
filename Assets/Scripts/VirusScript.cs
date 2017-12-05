using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Virus script.
/// Control the movements of the Viruses.
/// </summary>
public class VirusScript : MonoBehaviour {
	private Transform thisTransf;
	public GameObject virus;
	public float speed = 1;
	public float spinSpeed = 1;
	// Use this for initialization
	void Start () {
		thisTransf = transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward, spinSpeed * Time.deltaTime); //rotates virus and makes it spin around as it moves
		thisTransf.Translate (0, 0, -speed * Time.deltaTime);
	}
		
}
